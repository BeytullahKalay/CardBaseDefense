
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{
    private NavMeshAgent _agent;
    private Transform _transform;

    public EnemyIdleState(Transform transform,NavMeshAgent agent)
    {
        _transform = transform;
        _agent = agent;
    }
    public override void OnEnter(RangedEnemyStateManager stateManager)
    {
    }

    public override void OnUpdate(RangedEnemyStateManager stateManager)
    {
        if (GameManager.Instance.BaseTransform != null)
        {
            stateManager.SwitchState(stateManager.EnemyMoveState);
        }
        else
        {
            _agent.SetDestination(_transform.position);
        }
    }

    public override void OnExit(RangedEnemyStateManager stateManager)
    {
    }
}