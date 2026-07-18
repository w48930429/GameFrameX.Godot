using Godot;
using GameFrameX.Runtime;

namespace Godot.Hotfix.Game.Component
{
    public partial class FightComponent : GameFrameworkComponent
    {
        public override void _Ready()
        {
            IsAutoRegister = false;
            base._Ready();
            GD.Print("[FightComponent] Ready (skeleton)");
        }
    }
}