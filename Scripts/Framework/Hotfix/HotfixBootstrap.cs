using System;
using System.Linq;
using Godot;

namespace Godot.Startup.Hotfix
{
    public partial class HotfixBootstrap : Node
    {
        private static readonly (string typeName, string scriptPath)[] Components = new[]
        {
            ("Godot.Hotfix.Game.Component.GameConstantComponent", "res://Assets/Hotfix/Game/Component/GameConstantComponent.cs"),
            ("Godot.Hotfix.Game.Component.SaveComponent", "res://Assets/Hotfix/Game/Component/SaveComponent.cs"),
            ("Godot.Hotfix.Game.Component.FightComponent", "res://Assets/Hotfix/Game/Component/FightComponent.cs"),
            ("Godot.Hotfix.Game.Component.GameLoopComponent", "res://Assets/Hotfix/Game/Component/GameLoopComponent.cs"),
        };

        public override void _Ready()
        {
            var hotfixAssembly = LoadHotfixAssembly();
            RegisterHotfixScriptPathAliases(hotfixAssembly);
            CreateGameComponents(hotfixAssembly);
            GD.Print("[HotfixBootstrap] Ready");
        }

        private System.Reflection.Assembly LoadHotfixAssembly()
        {
            var hotfixAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(static m => string.Equals(m.GetName().Name, "Hotfix", StringComparison.Ordinal));
            if (hotfixAssembly != null)
            {
                Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(hotfixAssembly);
                return hotfixAssembly;
            }

            var path = ProjectSettings.GlobalizePath("user://hotfix/Hotfix.dll");
            if (!System.IO.File.Exists(path))
            {
                GD.PrintErr($"[HotfixBootstrap] Hotfix.dll not found: {path}");
                return null;
            }

            try
            {
                var alc = System.Runtime.Loader.AssemblyLoadContext.GetLoadContext(
                    System.Reflection.Assembly.GetExecutingAssembly()) ?? System.Runtime.Loader.AssemblyLoadContext.Default;
                hotfixAssembly = alc.LoadFromAssemblyPath(path);
                Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(hotfixAssembly);
                GD.Print($"[HotfixBootstrap] Loaded Hotfix from: {path}");
                return hotfixAssembly;
            }
            catch (Exception e)
            {
                GD.PrintErr($"[HotfixBootstrap] Failed to load Hotfix: {e.Message}");
                return null;
            }
        }

        private static void RegisterHotfixScriptPathAliases(System.Reflection.Assembly hotfixAssembly)
        {
            if (hotfixAssembly == null) return;
            try
            {
                var bridgeType = typeof(Godot.Bridge.ScriptManagerBridge);
                var pathTypeBiMapField = bridgeType.GetField("_pathTypeBiMap",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                if (pathTypeBiMapField == null) return;

                object pathTypeBiMap = pathTypeBiMapField.GetValue(null);
                if (pathTypeBiMap == null) return;

                var addMethod = pathTypeBiMap.GetType().GetMethod("Add",
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (addMethod == null) return;

                const string hotfixPrefix = "res://Assets/Hotfix/";
                int aliasCount = 0;

                foreach (var type in hotfixAssembly.GetTypes())
                {
                    if (type.IsNested || type.IsAbstract) continue;
                    if (!typeof(GodotObject).IsAssignableFrom(type)) continue;

                    var scriptPathAttr = type.GetCustomAttributes(inherit: false)
                        .OfType<ScriptPathAttribute>()
                        .FirstOrDefault();
                    if (scriptPathAttr == null) continue;

                    string originalPath = scriptPathAttr.Path;
                    if (!originalPath.StartsWith("res://") || originalPath.StartsWith(hotfixPrefix)) continue;

                    string aliasedPath = hotfixPrefix + originalPath.Substring("res://".Length);
                    try
                    {
                        addMethod.Invoke(pathTypeBiMap, new object[] { aliasedPath, type });
                        aliasCount++;
                    }
                    catch (System.Reflection.TargetInvocationException tie)
                    {
                        if (tie.InnerException is not ArgumentException) throw;
                    }
                }

                if (aliasCount > 0)
                    GD.Print($"[HotfixBootstrap] Registered {aliasCount} script path aliases");
            }
            catch (Exception e)
            {
                GD.PrintErr($"[HotfixBootstrap] RegisterHotfixScriptPathAliases failed: {e.Message}");
            }
        }

        private void CreateGameComponents(System.Reflection.Assembly hotfixAssembly)
        {
            if (hotfixAssembly == null) return;
            foreach (var (typeName, scriptPath) in Components)
            {
                var script = GD.Load<CSharpScript>(scriptPath);
                if (script == null)
                {
                    GD.PrintErr($"[HotfixBootstrap] Failed to load script: {scriptPath}");
                    continue;
                }
                var instance = (Node)script.New();
                if (instance == null)
                {
                    GD.PrintErr($"[HotfixBootstrap] Failed to instantiate: {typeName}");
                    continue;
                }
                instance.Name = typeName.Substring(typeName.LastIndexOf('.') + 1);
                CallDeferred(nameof(AddRootChild), instance);
            }
        }

        private void AddRootChild(Node node)
        {
            GetTree().Root.AddChild(node);
            GD.Print($"[HotfixBootstrap] Added {node.Name} as root child");
        }
    }
}
