using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public class ItemData
    {
        public int Id;
        public int Count;

        public GodotDict ToDictionary()
        {
            return new GodotDict
            {
                { "id", Id },
                { "num", Count },
            };
        }

        public static ItemData FromDictionary(GodotDict dict)
        {
            var data = new ItemData();
            if (dict.TryGetValue("id", out var id)) data.Id = (int)id;
            if (dict.TryGetValue("num", out var num)) data.Count = (int)num;
            return data;
        }
    }
}
