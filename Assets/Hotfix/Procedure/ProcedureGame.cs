using Godot;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;

namespace Godot.Startup.Procedure
{
    public class ProcedureGame : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GD.Print("[ProcedureGame] OnEnter - 进入游戏主流程");
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }
    }
}
