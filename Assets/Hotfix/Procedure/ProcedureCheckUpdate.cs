using Godot;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;

namespace Godot.Startup.Procedure
{
    public class ProcedureCheckUpdate : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GD.Print("[ProcedureCheckUpdate] OnEnter - 跳过更新检查，直接进入登录");
            ChangeState<ProcedureLogin>(procedureOwner);
        }
    }
}