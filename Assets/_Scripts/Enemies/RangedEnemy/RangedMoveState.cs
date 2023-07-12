using UnityEngine;
using UnityEngine.AI;

public class RangedMoveState : RangedBaseState
{
    private NavMeshAgent _agent;
    private Transform _transform;

    public RangedMoveState(NavMeshAgent agent,Transform transform)
    {
        _agent = agent;
        _transform = transform;
    }
    
    public override void OnEnter(RangedStateManager stateManager)
    {
        stateManager.UnitStates = UnitStates.Move;
    }

    public override void OnUpdate(RangedStateManager stateManager)
    {
        if (stateManager.DetectTargets().Capacity > 0)
        {
            stateManager.SwitchState(stateManager.RangedAttackState);
        }
        else if (GameManager.Instance.BaseTransform != null)
        {
            _agent.SetDestination(stateManager.MovePos);
        }
        else
        {
            _agent.SetDestination(_transform.position);
            stateManager.SwitchState(stateManager.RangedIdleState);
        }
    }

    public override void OnExit(RangedStateManager stateManager)
    {
    }
    
    
}
