
using UnityEngine;

public class RangedIdleState : RangedBaseState
{
    public override void OnEnter(RangedStateManager stateManager)
    {
        stateManager.UnitStates = UnitStates.Idle;
    }

    public override void OnUpdate(RangedStateManager stateManager)
    {
        if (stateManager.BoardState == BoardStates.Landed && GameManager.Instance.BaseTransform != null)
        {
            stateManager.SwitchState(stateManager.RangedMoveState);
        }
    }

    public override void OnExit(RangedStateManager stateManager)
    {
    }
}