
public class PuncherEnemyIdleState : PuncherEnemyBaseState
{
    public override void OnEnter(PuncherEnemyStateManager stateManager)
    {
    }

    public override void OnUpdate(PuncherEnemyStateManager stateManager)
    {
        if (stateManager.BoardState == BoardStates.Landed && GameManager.Instance.BaseTransform != null)
        {
            stateManager.SwitchState(stateManager.PuncherEnemyMoveState);
        }
    }

    public override void OnExit(PuncherEnemyStateManager stateManager)
    {
    }
}
