using Godot;
using GameFrameX.Runtime;

namespace Godot.Hotfix.Game.Component
{
    public partial class GameLoopComponent : GameFrameworkComponent
    {
        public override void _Ready()
        {
            IsAutoRegister = false;
            base._Ready();
            GD.Print("[GameLoopComponent] Ready (skeleton)");
        }
    }
}