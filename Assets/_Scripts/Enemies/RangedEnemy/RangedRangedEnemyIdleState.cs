
public class RangedRangedEnemyIdleState : RangedEnemyBaseState
{
    public override void OnEnter(RangedEnemyStateManager stateManager)
    {
        stateManager.UnitStates = UnitStates.Idle;
    }

    public override void OnUpdate(RangedEnemyStateManager stateManager)
    {
        if (stateManager.BoardState == BoardStates.Landed && GameManager.Instance.BaseTransform != null)
        {
            stateManager.SwitchState(stateManager.RangedRangedEnemyMoveState);
        }

    }

    public override void OnExit(RangedEnemyStateManager stateManager)
    {
    }
}