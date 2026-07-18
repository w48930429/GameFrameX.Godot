using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public class PlayerState
    {
        public long Gold;
        public float Exp;
        public int MapIndex;
        public int MapLevel;
        public int MapProgress;

        public GodotDict ToDictionary()
        {
            return new GodotDict
            {
                { "gold", Gold },
                { "exp", Exp },
                { "map_index", MapIndex },
                { "map_level", MapLevel },
                { "map_progress", MapProgress },
            };
        }

        public static PlayerState FromDictionary(GodotDict dict)
        {
            var data = new PlayerState();
            if (dict.TryGetValue("gold", out var gold)) data.Gold = (long)gold;
            if (dict.TryGetValue("exp", out var exp)) data.Exp = (float)exp;
            if (dict.TryGetValue("map_index", out var mapIndex)) data.MapIndex = (int)mapIndex;
            if (dict.TryGetValue("map_level", out var mapLevel)) data.MapLevel = (int)mapLevel;
            if (dict.TryGetValue("map_progress", out var mapProgress)) data.MapProgress = (int)mapProgress;
            return data;
        }
    }
}
