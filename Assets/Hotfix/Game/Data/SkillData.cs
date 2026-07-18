using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public class SkillData
    {
        public int SkillId;
        public int Level;

        public GodotDict ToDictionary()
        {
            return new GodotDict
            {
                { "id", SkillId },
                { "lv", Level },
            };
        }

        public static SkillData FromDictionary(GodotDict dict)
        {
            var data = new SkillData();
            if (dict.TryGetValue("id", out var id)) data.SkillId = (int)id;
            if (dict.TryGetValue("lv", out var lv)) data.Level = (int)lv;
            return data;
        }
    }
}
