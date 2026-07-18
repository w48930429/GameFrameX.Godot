using System.Collections.Generic;
using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public class SpiritData
    {
        public int SpiritId;
        public int Quality;
        public Dictionary<string, float> Attr = new();

        public GodotDict ToDictionary()
        {
            var dict = new GodotDict
            {
                { "id", SpiritId },
                { "ys", Quality },
            };
            if (Attr.Count > 0) dict["attr"] = EquipmentData.ToGodotDict(Attr);
            return dict;
        }

        public static SpiritData FromDictionary(GodotDict dict)
        {
            var data = new SpiritData();
            if (dict.TryGetValue("id", out var id)) data.SpiritId = (int)id;
            if (dict.TryGetValue("ys", out var ys)) data.Quality = (int)ys;
            if (dict.TryGetValue("attr", out var attrObj))
                data.Attr = EquipmentData.FromGodotDict(attrObj.AsGodotDictionary());
            return data;
        }
    }
}
