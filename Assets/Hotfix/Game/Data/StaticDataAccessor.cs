using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public static class StaticDataAccessor
    {
        private static GodotDict _monsterData;
        private static GodotDict _mapData;
        private static GodotDict _allData;
        private static GodotDict _buildData;
        private static GodotDict _skillData;
        private static GodotDict _tzData;
        private static GodotDict _spiritData;
        private static GodotDict _achData;

        public static GodotDict MonsterData => _monsterData ??= LoadEncryptedJson("Moster.json");
        public static GodotDict MapData => _mapData ??= LoadEncryptedJson("Map.json");
        public static GodotDict AllData => _allData ??= LoadEncryptedJson("Data.json");
        public static GodotDict BuildData => _buildData ??= LoadEncryptedJson("BuildData.json");
        public static GodotDict SkillData => _skillData ??= LoadEncryptedJson("SkillData.json");
        public static GodotDict TzData => _tzData ??= LoadEncryptedJson("TzData.json");
        public static GodotDict SpiritData => _spiritData ??= LoadEncryptedJson("Spirit.json");
        public static GodotDict AchData => _achData ??= LoadEncryptedJson("AchData.json");

        public static GodotDict GetMonster(Variant id)
        {
            return GetFromDict(MonsterData, id);
        }

        public static GodotDict GetMap(Variant id)
        {
            return GetFromDict(MapData, id);
        }

        public static GodotDict GetItem(Variant id)
        {
            return GetFromDict(AllData, id);
        }

        public static GodotDict GetBuild(Variant id)
        {
            return GetFromDict(BuildData, id);
        }

        public static GodotDict GetSkill(Variant id)
        {
            return GetFromDict(SkillData, id);
        }

        public static GodotDict GetSuit(Variant id)
        {
            return GetFromDict(TzData, id);
        }

        public static GodotDict GetSpirit(Variant id)
        {
            return GetFromDict(SpiritData, id);
        }

        public static GodotDict GetAch(Variant id)
        {
            return GetFromDict(AchData, id);
        }

        private static GodotDict GetFromDict(GodotDict dict, Variant key)
        {
            if (dict != null && dict.TryGetValue(key, out var val))
            {
                var d = val.AsGodotDictionary();
                if (d != null) return d;
            }
            return null;
        }

        private static GodotDict LoadEncryptedJson(string fileName)
        {
            var path = "res://Assets/Hotfix/Storage/" + fileName;

            using var plainFile = FileAccess.Open(path, FileAccess.ModeFlags.Read);
            if (plainFile != null)
            {
                var plainJson = plainFile.GetAsText();
                var plainResult = Json.ParseString(plainJson);
                var plainDict = plainResult.AsGodotDictionary();
                if (plainDict != null)
                    return plainDict;
            }

            using var encFile = FileAccess.OpenEncryptedWithPass(path, FileAccess.ModeFlags.Read, "sakuya");
            if (encFile != null)
            {
                var json = encFile.GetAsText();
                var result = Json.ParseString(json);
                var dict = result.AsGodotDictionary();
                if (dict != null)
                    return dict;
            }

            GD.PrintErr($"[StaticDataAccessor] Failed to load: {path}");
            return new GodotDict();
        }
    }
}
