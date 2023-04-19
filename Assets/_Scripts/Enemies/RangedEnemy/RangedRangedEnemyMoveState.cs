using UnityEngine;
using UnityEngine.AI;

public class RangedRangedEnemyMoveState : RangedEnemyBaseState
{
    private NavMeshAgent _agent;
    private Transform _transform;

    public RangedRangedEnemyMoveState(NavMeshAgent agent,Transform transform)
    {
        _agent = agent;
        _transform = transform;
    }
    
    public override void OnEnter(RangedEnemyStateManager stateManager)
    {
        stateManager.UnitStates = UnitStates.Move;
    }

    public override void OnUpdate(RangedEnemyStateManager stateManager)
    {
        if (stateManager.DetectTargets().Capacity > 0)
        {
            stateManager.SwitchState(stateManager.RangedRangedEnemyAttackState);
        }
        else if (GameManager.Instance.BaseTransform != null)
        {
            _agent.SetDestination(GameManager.Instance.BaseTransform.position);
        }
        else
        {
            _agent.SetDestination(_transform.position);
            stateManager.SwitchState(stateManager.RangedRangedEnemyIdleState);
        }
    }

    public override void OnExit(RangedEnemyStateManager stateManager)
    {
    }
    
    
}
