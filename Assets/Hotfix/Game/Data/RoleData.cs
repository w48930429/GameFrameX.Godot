using System.Collections.Generic;
using Godot;
using GodotDict = Godot.Collections.Dictionary;
using GodotArray = Godot.Collections.Array;

namespace Godot.Hotfix.Game.Data
{
    public class RoleData
    {
        public long Uid;
        public int Job;
        public int Level;
        public float Exp;
        public Dictionary<string, float> BaseAttr = new();
        public List<long> EquippedIds = new();
        public List<int> SkillIds = new();
        public int SpiritId;

        public GodotDict ToDictionary()
        {
            var dict = new GodotDict
            {
                { "uid", Uid },
                { "job", Job },
                { "lv", Level },
                { "exp", Exp },
                { "spirit", SpiritId },
            };

            var attrDict = new GodotDict();
            foreach (var kv in BaseAttr)
                attrDict[kv.Key] = kv.Value;
            dict["attr"] = attrDict;

            var equArr = new GodotArray();
            foreach (var id in EquippedIds) equArr.Add(id);
            dict["equ"] = equArr;

            var skillArr = new GodotArray();
            foreach (var id in SkillIds) skillArr.Add(id);
            dict["skill"] = skillArr;

            return dict;
        }

        public static RoleData FromDictionary(GodotDict dict)
        {
            var data = new RoleData();
            if (dict.TryGetValue("uid", out var uid)) data.Uid = (long)uid;
            if (dict.TryGetValue("job", out var job)) data.Job = (int)job;
            if (dict.TryGetValue("lv", out var lv)) data.Level = (int)lv;
            if (dict.TryGetValue("exp", out var exp)) data.Exp = (float)exp;
            if (dict.TryGetValue("spirit", out var spirit)) data.SpiritId = (int)spirit;

            if (dict.TryGetValue("attr", out var attrObj))
            {
                var attrDict = attrObj.AsGodotDictionary();
                if (attrDict != null)
                {
                    foreach (var key in attrDict.Keys)
                    {
                        var keyStr = key.ToString();
                        if (keyStr != null && attrDict[key].AsSingle() is float val)
                            data.BaseAttr[keyStr] = val;
                    }
                }
            }

            if (dict.TryGetValue("equ", out var equObj))
            {
                var equArr = equObj.AsGodotArray();
                if (equArr != null)
                    foreach (var item in equArr)
                        data.EquippedIds.Add((long)item);
            }

            if (dict.TryGetValue("skill", out var skillObj))
            {
                var skillArr = skillObj.AsGodotArray();
                if (skillArr != null)
                    foreach (var item in skillArr)
                        data.SkillIds.Add((int)item);
            }

            return data;
        }
    }
}
