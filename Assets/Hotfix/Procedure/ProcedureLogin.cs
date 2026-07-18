using Godot;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;

namespace Godot.Startup.Procedure
{
    public class ProcedureLogin : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GD.Print("[ProcedureLogin] OnEnter - 本地离线进入，跳过登录");
            ChangeState<ProcedureGame>(procedureOwner);
        }
    }
}