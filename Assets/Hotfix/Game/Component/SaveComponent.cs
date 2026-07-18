using System;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using Godot.Hotfix.Game.Data;
using GameFrameX.Runtime;

namespace Godot.Hotfix.Game.Component
{
    public partial class SaveComponent : GameFrameworkComponent
    {
        private const string EncryptKey = "sakuya";
        private const string SavePath = "user://Storages.json";
        private const string BackupPath = "user://Storages_copy.json";

        private SemaphoreSlim _semaphore = new(0, 1);
        private PlayerSaveData _pendingData;
        private bool _isSaving;
        private Task _saveTask;

        public PlayerSaveData Data { get; private set; }

        public override void _Ready()
        {
            IsAutoRegister = false;
            base._Ready();
            Data = Load();
            GD.Print($"[SaveComponent] Loaded save data, Gold={Data?.State?.Gold ?? 0}");
            _saveTask = Task.Run(SaveLoop);
        }

        public void Save(PlayerSaveData data)
        {
            _pendingData = data;
            _semaphore.Release();
        }

        private async Task SaveLoop()
        {
            while (true)
            {
                await _semaphore.WaitAsync();
                _isSaving = true;
                try
                {
                    WriteToFile(SavePath, _pendingData);
                    WriteToFile(BackupPath, _pendingData);
                    Data = _pendingData;
                }
                catch (Exception e)
                {
                    GD.PrintErr($"[SaveComponent] Save failed: {e.Message}");
                }
                _isSaving = false;
            }
        }

        private void WriteToFile(string path, PlayerSaveData data)
        {
            var json = Json.Stringify(data.ToDictionary());
            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
            if (file != null)
            {
                file.StoreString(json);
            }
        }

        private PlayerSaveData Load()
        {
            var data = TryLoadFrom(SavePath);
            if (data == null) data = TryLoadFrom(BackupPath);
            return data ?? new PlayerSaveData();
        }

        private PlayerSaveData TryLoadFrom(string path)
        {
            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
            if (file == null) return null;
            var json = file.GetAsText();
            var result = Json.ParseString(json);
            if (result.VariantType == Variant.Type.Dictionary)
            {
                var dict = result.AsGodotDictionary();
                if (dict != null)
                    return PlayerSaveData.FromDictionary(dict);
            }
            return null;
        }

        public override void _ExitTree()
        {
            if (_pendingData != null)
            {
                WriteToFile(SavePath, _pendingData);
                WriteToFile(BackupPath, _pendingData);
            }
            base._ExitTree();
        }
    }
}