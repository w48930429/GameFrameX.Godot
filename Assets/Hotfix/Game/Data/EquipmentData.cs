using System.Collections.Generic;
using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public class EquipmentData
    {
        public long Uid;
        public int BuildId;
        public int BuildType;
        public int Quality;
        public Dictionary<string, float> BaseAttr = new();
        public Dictionary<string, float> SealAttr = new();
        public Dictionary<string, float> SuperAttr = new();
        public Dictionary<string, float> ElementAttr = new();
        public int SuitId;

        public GodotDict ToDictionary()
        {
            var dict = new GodotDict
            {
                { "uid", Uid },
                { "build_id", BuildId },
                { "build_type", BuildType },
                { "ys", Quality },
                { "tz", SuitId },
            };

            if (BaseAttr.Count > 0) dict["base_attr"] = ToGodotDict(BaseAttr);
            if (SealAttr.Count > 0) dict["seal"] = ToGodotDict(SealAttr);
            if (SuperAttr.Count > 0) dict["super_attr"] = ToGodotDict(SuperAttr);
            if (ElementAttr.Count > 0) dict["ys_attr"] = ToGodotDict(ElementAttr);

            return dict;
        }

        public static EquipmentData FromDictionary(GodotDict dict)
        {
            var data = new EquipmentData();
            if (dict.TryGetValue("uid", out var uid)) data.Uid = (long)uid;
            if (dict.TryGetValue("build_id", out var buildId)) data.BuildId = (int)buildId;
            if (dict.TryGetValue("build_type", out var buildType)) data.BuildType = (int)buildType;
            if (dict.TryGetValue("ys", out var ys)) data.Quality = (int)ys;
            if (dict.TryGetValue("tz", out var tz)) data.SuitId = (int)tz;

            if (dict.TryGetValue("base_attr", out var baseObj))
                data.BaseAttr = FromGodotDict(baseObj.AsGodotDictionary());
            if (dict.TryGetValue("seal", out var sealObj))
                data.SealAttr = FromGodotDict(sealObj.AsGodotDictionary());
            if (dict.TryGetValue("super_attr", out var superObj))
                data.SuperAttr = FromGodotDict(superObj.AsGodotDictionary());
            if (dict.TryGetValue("ys_attr", out var ysObj))
                data.ElementAttr = FromGodotDict(ysObj.AsGodotDictionary());

            return data;
        }

        internal static GodotDict ToGodotDict(Dictionary<string, float> attr)
        {
            var d = new GodotDict();
            foreach (var kv in attr) d[kv.Key] = kv.Value;
            return d;
        }

        internal static Dictionary<string, float> FromGodotDict(GodotDict dict)
        {
            var result = new Dictionary<string, float>();
            if (dict == null) return result;
            foreach (var key in dict.Keys)
            {
                var keyStr = key.ToString();
                if (keyStr != null && dict[key].AsSingle() is float val)
                    result[keyStr] = val;
            }
            return result;
        }
    }
}
