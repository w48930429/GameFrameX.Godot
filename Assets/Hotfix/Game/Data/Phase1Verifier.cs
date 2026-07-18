using System.Collections.Generic;
using Godot;
using GodotDict = Godot.Collections.Dictionary;

namespace Godot.Hotfix.Game.Data
{
    public static class Phase1Verifier
    {
        public static void Run()
        {
            var ok = true;

            ok &= VerifyPlayerSaveDataRoundTrip();
            ok &= VerifyEventIdConstants();
            ok &= VerifyEventArgsCreateAndClear();

            if (ok)
                GD.Print("[Phase1] ALL PASSED");
            else
                GD.PrintErr("[Phase1] SOME CHECKS FAILED");
        }

        private static bool VerifyPlayerSaveDataRoundTrip()
        {
            var original = new PlayerSaveData();
            original.State.Gold = 100;
            original.State.MapLevel = 5;
            original.Team.Add(new RoleData { Uid = 1, Job = 0, Level = 5, Exp = 50f, SpiritId = 10 });
            original.Equipments.Add(new EquipmentData { Uid = 100, BuildId = 200, Quality = 3 });
            original.Inventory.Add(new ItemData { Id = 300, Count = 5 });
            original.Seals.Add(new SealData { Uid = 1, SealId = 400 });
            original.Suits.Add(new SuitData { SuitId = 500 });
            original.Skills.Add(new SkillData { SkillId = 600, Level = 3 });
            original.Spirits.Add(new SpiritData { SpiritId = 700, Quality = 4 });

            var dict = original.ToDictionary();
            var restored = PlayerSaveData.FromDictionary(dict);

            bool pass = true;

            if (restored.State.Gold != 100) { GD.PrintErr("[Phase1] State.Gold mismatch"); pass = false; }
            if (restored.State.MapLevel != 5) { GD.PrintErr("[Phase1] State.MapLevel mismatch"); pass = false; }
            if (restored.Team.Count != 1 || restored.Team[0].Uid != 1) { GD.PrintErr("[Phase1] Team mismatch"); pass = false; }
            if (restored.Equipments.Count != 1 || restored.Equipments[0].Uid != 100) { GD.PrintErr("[Phase1] Equipments mismatch"); pass = false; }
            if (restored.Inventory.Count != 1 || restored.Inventory[0].Id != 300) { GD.PrintErr("[Phase1] Inventory mismatch"); pass = false; }
            if (restored.Seals.Count != 1 || restored.Seals[0].SealId != 400) { GD.PrintErr("[Phase1] Seals mismatch"); pass = false; }
            if (restored.Suits.Count != 1 || restored.Suits[0].SuitId != 500) { GD.PrintErr("[Phase1] Suits mismatch"); pass = false; }
            if (restored.Skills.Count != 1 || restored.Skills[0].SkillId != 600) { GD.PrintErr("[Phase1] Skills mismatch"); pass = false; }
            if (restored.Spirits.Count != 1 || restored.Spirits[0].SpiritId != 700) { GD.PrintErr("[Phase1] Spirits mismatch"); pass = false; }

            if (pass) GD.Print("[Phase1] PlayerSaveData round-trip PASSED");
            return pass;
        }

        private static bool VerifyEventIdConstants()
        {
            bool pass = true;
            var ids = new Dictionary<string, string>
            {
                { "OnAttackOver", "Game.OnAttackOver" },
                { "OnAttributeChange", "Game.OnAttributeChange" },
                { "OnRoleDie", "Game.OnRoleDie" },
                { "OnRoleHurt", "Game.OnRoleHurt" },
                { "OnFightResult", "Game.OnFightResult" },
                { "OnMapProgress", "Game.OnMapProgress" },
                { "OnEncounter", "Game.OnEncounter" },
                { "OnLevelUp", "Game.OnLevelUp" },
                { "OnItemObtained", "Game.OnItemObtained" },
                { "OnEquipmentObtained", "Game.OnEquipmentObtained" },
            };

            foreach (var kv in ids)
            {
                var field = typeof(Event.EventId).GetField(kv.Key);
                if (field == null || (string)field.GetValue(null) != kv.Value)
                {
                    GD.PrintErr($"[Phase1] EventId.{kv.Key} mismatch");
                    pass = false;
                }
            }

            if (pass) GD.Print("[Phase1] EventId constants PASSED");
            return pass;
        }

        private static bool VerifyEventArgsCreateAndClear()
        {
            bool pass = true;

            var attackOver = Event.AttackOverEventArgs.Create(42);
            if (attackOver.AttackerUid != 42 || attackOver.Id != Event.EventId.OnAttackOver) { GD.PrintErr("[Phase1] AttackOverEventArgs mismatch"); pass = false; }
            attackOver.Clear();
            if (attackOver.AttackerUid != 0) { GD.PrintErr("[Phase1] AttackOverEventArgs.Clear failed"); pass = false; }

            var roleDie = Event.RoleDieEventArgs.Create(99, true);
            if (roleDie.RoleUid != 99 || !roleDie.IsEnemy) { GD.PrintErr("[Phase1] RoleDieEventArgs mismatch"); pass = false; }
            roleDie.Clear();
            if (roleDie.RoleUid != 0 || roleDie.IsEnemy) { GD.PrintErr("[Phase1] RoleDieEventArgs.Clear failed"); pass = false; }

            var fightResult = Event.FightResultEventArgs.Create(true);
            if (!fightResult.Win) { GD.PrintErr("[Phase1] FightResultEventArgs mismatch"); pass = false; }
            fightResult.Clear();
            if (fightResult.Win) { GD.PrintErr("[Phase1] FightResultEventArgs.Clear failed"); pass = false; }

            var levelUp = Event.LevelUpEventArgs.Create(1, 5, 6);
            if (levelUp.RoleUid != 1 || levelUp.OldLevel != 5 || levelUp.NewLevel != 6) { GD.PrintErr("[Phase1] LevelUpEventArgs mismatch"); pass = false; }
            levelUp.Clear();
            if (levelUp.RoleUid != 0 || levelUp.OldLevel != 0 || levelUp.NewLevel != 0) { GD.PrintErr("[Phase1] LevelUpEventArgs.Clear failed"); pass = false; }

            if (pass) GD.Print("[Phase1] EventArgs Create/Clear PASSED");
            return pass;
        }
    }
}
