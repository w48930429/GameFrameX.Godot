using Godot;
using GameFrameX.Runtime;

namespace Godot.Hotfix.Game.Component
{
    public partial class GameConstantComponent : GameFrameworkComponent
    {
        public enum HurtType { Other = -1, Atk = 0, Mtk = 1, True = 2, Blood = 3, Miss = 4, Hold = 5, Crit = 6, Continued = 7, Exp = 8 }
        public enum BuffMode { Buff = 0, Debuff = 1, State = 2 }
        public enum BuffState { Other = -1, Vertigo = 0, Blinding = 1, Continued = 2, Weak = 3, Sj = 4, NoHurt = 5 }
        public enum SkillTriggerType { Odds = 0, Attr = 1, AttackHit = 2, PerSecond = 3 }
        public enum SkillHurtMode { Number = 0, PtAttrOn = 1, PtAttrOt = 2 }
        public enum Quality { C = 0, B = 1, A = 2, S = 3, SPlus = 4, Legend = 5, Myth = 6 }
        public enum AttrType { MaxHp, Hp, Atk, Mtk, Def, MDef, Speed, Crit, UnCrit, Hold, HoldNum, Dodge, HoldPass, MtkPass, AtkPass, AtkBlood, MtkBlood, AtkBuff, MtkBuff, HpBuff, HurtBuff, CritBuff, SkillBuff, AtkHurtBuff, MtkHurtBuff, TrueHurt, Shield, ShieldBuff, ExpBuff, Reflex, SkillCrit, UnPt, AtkMtk, HurtPass, Fire, Wind, Ice, Poison }
        public enum RoleWalkStatus { Walk, Idle, Atk, Die, WaitAtk, Fb }
        public enum WorldMode { Nor, Ys }

        [Export] public int[] FightArray = { 3, 4, 5, 6 };
        [Export] public int ArrayNum = 0;
        [Export] public bool IsFightNum = true;

        public override void _Ready()
        {
            IsAutoRegister = false;
            base._Ready();
            GD.Print($"[GameConstant] FightArray=[{string.Join(",", FightArray)}] ArrayNum={ArrayNum} IsFightNum={IsFightNum}");
        }
    }
}