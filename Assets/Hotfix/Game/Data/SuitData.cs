using System.Collections.Generic;
using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public class SuitData
    {
        public int SuitId;
        public Dictionary<string, float> Attr = new();

        public GodotDict ToDictionary()
        {
            var dict = new GodotDict
            {
                { "id", SuitId },
            };
            if (Attr.Count > 0) dict["attr"] = EquipmentData.ToGodotDict(Attr);
            return dict;
        }

        public static SuitData FromDictionary(GodotDict dict)
        {
            var data = new SuitData();
            if (dict.TryGetValue("id", out var id)) data.SuitId = (int)id;
            if (dict.TryGetValue("attr", out var attrObj))
                data.Attr = EquipmentData.FromGodotDict(attrObj.AsGodotDictionary());
            return data;
        }
    }
}
