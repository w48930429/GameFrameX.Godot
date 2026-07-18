using GameFrameX.Event.Runtime;

namespace Godot.Hotfix.Game.Event
{
    public class AttackOverEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnAttackOver;
        public long AttackerUid;

        public override void Clear()
        {
            AttackerUid = 0;
        }

        public static AttackOverEventArgs Create(long attackerUid)
        {
            var args = new AttackOverEventArgs { AttackerUid = attackerUid };
            return args;
        }
    }

    public class AttributeChangeEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnAttributeChange;
        public int AttrTypeIndex;
        public float OldValue;
        public float NewValue;

        public override void Clear()
        {
            AttrTypeIndex = 0;
            OldValue = 0f;
            NewValue = 0f;
        }

        public static AttributeChangeEventArgs Create(int attrTypeIndex, float oldValue, float newValue)
        {
            var args = new AttributeChangeEventArgs { AttrTypeIndex = attrTypeIndex, OldValue = oldValue, NewValue = newValue };
            return args;
        }
    }

    public class RoleDieEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnRoleDie;
        public long RoleUid;
        public bool IsEnemy;

        public override void Clear()
        {
            RoleUid = 0;
            IsEnemy = false;
        }

        public static RoleDieEventArgs Create(long roleUid, bool isEnemy)
        {
            var args = new RoleDieEventArgs { RoleUid = roleUid, IsEnemy = isEnemy };
            return args;
        }
    }

    public class RoleHurtEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnRoleHurt;
        public long TargetUid;
        public float Damage;
        public int HurtTypeIndex;

        public override void Clear()
        {
            TargetUid = 0;
            Damage = 0f;
            HurtTypeIndex = 0;
        }

        public static RoleHurtEventArgs Create(long targetUid, float damage, int hurtTypeIndex)
        {
            var args = new RoleHurtEventArgs { TargetUid = targetUid, Damage = damage, HurtTypeIndex = hurtTypeIndex };
            return args;
        }
    }

    public class FightResultEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnFightResult;
        public bool Win;

        public override void Clear()
        {
            Win = false;
        }

        public static FightResultEventArgs Create(bool win)
        {
            var args = new FightResultEventArgs { Win = win };
            return args;
        }
    }

    public class MapProgressEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnMapProgress;
        public int Progress;

        public override void Clear()
        {
            Progress = 0;
        }

        public static MapProgressEventArgs Create(int progress)
        {
            var args = new MapProgressEventArgs { Progress = progress };
            return args;
        }
    }

    public class EncounterEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnEncounter;
        public bool IsBoss;

        public override void Clear()
        {
            IsBoss = false;
        }

        public static EncounterEventArgs Create(bool isBoss)
        {
            var args = new EncounterEventArgs { IsBoss = isBoss };
            return args;
        }
    }

    public class LevelUpEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnLevelUp;
        public long RoleUid;
        public int OldLevel;
        public int NewLevel;

        public override void Clear()
        {
            RoleUid = 0;
            OldLevel = 0;
            NewLevel = 0;
        }

        public static LevelUpEventArgs Create(long roleUid, int oldLevel, int newLevel)
        {
            var args = new LevelUpEventArgs { RoleUid = roleUid, OldLevel = oldLevel, NewLevel = newLevel };
            return args;
        }
    }

    public class ItemObtainedEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnItemObtained;
        public int ItemId;
        public int Count;

        public override void Clear()
        {
            ItemId = 0;
            Count = 0;
        }

        public static ItemObtainedEventArgs Create(int itemId, int count)
        {
            var args = new ItemObtainedEventArgs { ItemId = itemId, Count = count };
            return args;
        }
    }

    public class EquipmentObtainedEventArgs : GameEventArgs
    {
        public override string Id => EventId.OnEquipmentObtained;
        public long EquipmentUid;
        public int Quality;

        public override void Clear()
        {
            EquipmentUid = 0;
            Quality = 0;
        }

        public static EquipmentObtainedEventArgs Create(long equipmentUid, int quality)
        {
            var args = new EquipmentObtainedEventArgs { EquipmentUid = equipmentUid, Quality = quality };
            return args;
        }
    }
}