
public class PuncherEnemyIdleState : PuncherEnemyBaseState
{
    public override void OnEnter(PuncherStateManager stateManager)
    {
    }

    public override void OnUpdate(PuncherStateManager stateManager)
    {
        if (stateManager.BoardState == BoardStates.Landed && GameManager.Instance.BaseTransform != null)
        {
            stateManager.SwitchState(stateManager.PuncherEnemyMoveState);
        }
    }

    public override void OnExit(PuncherStateManager stateManager)
    {
    }
}
