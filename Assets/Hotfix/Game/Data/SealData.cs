using System.Collections.Generic;
using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public class SealData
    {
        public long Uid;
        public int SealId;
        public Dictionary<string, float> Attr = new();

        public GodotDict ToDictionary()
        {
            var dict = new GodotDict
            {
                { "uid", Uid },
                { "id", SealId },
            };
            if (Attr.Count > 0) dict["attr"] = EquipmentData.ToGodotDict(Attr);
            return dict;
        }

        public static SealData FromDictionary(GodotDict dict)
        {
            var data = new SealData();
            if (dict.TryGetValue("uid", out var uid)) data.Uid = (long)uid;
            if (dict.TryGetValue("id", out var id)) data.SealId = (int)id;
            if (dict.TryGetValue("attr", out var attrObj))
                data.Attr = EquipmentData.FromGodotDict(attrObj.AsGodotDictionary());
            return data;
        }
    }
}
