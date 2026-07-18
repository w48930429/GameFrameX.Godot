using Godot;
using GodotDict = Godot.Collections.Dictionary;
using GodotArray = Godot.Collections.Array;

namespace Godot.Hotfix.Game.Data
{
    public class PlayerSaveData
    {
        public PlayerState State = new();
        public System.Collections.Generic.List<RoleData> Team = new();
        public System.Collections.Generic.List<EquipmentData> Equipments = new();
        public System.Collections.Generic.List<ItemData> Inventory = new();
        public System.Collections.Generic.List<SealData> Seals = new();
        public System.Collections.Generic.List<SuitData> Suits = new();
        public System.Collections.Generic.List<SkillData> Skills = new();
        public System.Collections.Generic.List<SpiritData> Spirits = new();

        public GodotDict ToDictionary()
        {
            var dict = new GodotDict
            {
                { "player_state", State.ToDictionary() },
            };
            dict["team"] = ToGodotArray(Team, r => r.ToDictionary());
            dict["player_equipment"] = ToGodotArray(Equipments, e => e.ToDictionary());
            dict["player_inventory"] = ToGodotArray(Inventory, i => i.ToDictionary());
            dict["player_seal"] = ToGodotArray(Seals, s => s.ToDictionary());
            dict["player_tz"] = ToGodotArray(Suits, s => s.ToDictionary());
            dict["skill"] = ToGodotArray(Skills, s => s.ToDictionary());
            dict["spirit"] = ToGodotArray(Spirits, s => s.ToDictionary());
            return dict;
        }

        private static GodotArray ToGodotArray<T>(System.Collections.Generic.List<T> list, System.Func<T, GodotDict> converter)
        {
            var arr = new GodotArray();
            foreach (var item in list)
                arr.Add(converter(item));
            return arr;
        }

        public static PlayerSaveData FromDictionary(GodotDict dict)
        {
            var data = new PlayerSaveData();

            if (dict.TryGetValue("player_state", out var stateObj))
                data.State = PlayerState.FromDictionary(stateObj.AsGodotDictionary());

            if (dict.TryGetValue("team", out var teamObj))
                data.Team = ConvertList(teamObj.AsGodotArray(), RoleData.FromDictionary);

            if (dict.TryGetValue("player_equipment", out var equObj))
                data.Equipments = ConvertList(equObj.AsGodotArray(), EquipmentData.FromDictionary);

            if (dict.TryGetValue("player_inventory", out var invObj))
                data.Inventory = ConvertList(invObj.AsGodotArray(), ItemData.FromDictionary);

            if (dict.TryGetValue("player_seal", out var sealObj))
                data.Seals = ConvertList(sealObj.AsGodotArray(), SealData.FromDictionary);

            if (dict.TryGetValue("player_tz", out var tzObj))
                data.Suits = ConvertList(tzObj.AsGodotArray(), SuitData.FromDictionary);

            if (dict.TryGetValue("skill", out var skillObj))
                data.Skills = ConvertList(skillObj.AsGodotArray(), SkillData.FromDictionary);

            if (dict.TryGetValue("spirit", out var spiritObj))
                data.Spirits = ConvertList(spiritObj.AsGodotArray(), SpiritData.FromDictionary);

            return data;
        }

        private static System.Collections.Generic.List<T> ConvertList<T>(GodotArray arr, System.Func<GodotDict, T> converter)
        {
            var list = new System.Collections.Generic.List<T>();
            if (arr == null) return list;
            foreach (var item in arr)
            {
                var dict = item.AsGodotDictionary();
                if (dict != null)
                    list.Add(converter(dict));
            }
            return list;
        }
    }
}
