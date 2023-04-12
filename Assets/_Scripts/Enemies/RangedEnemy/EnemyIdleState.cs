
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{
    private NavMeshAgent _agent;
    private Transform _transform;
    private BoardStates _boardState;

    public EnemyIdleState(Transform transform,NavMeshAgent agent,ref BoardStates boardState)
    {
        _transform = transform;
        _agent = agent;
        _boardState = boardState;
    }
    public override void OnEnter(RangedEnemyStateManager stateManager)
    {
    }

    public override void OnUpdate(RangedEnemyStateManager stateManager)
    {
        if (_boardState != BoardStates.Landed || GameManager.Instance.BaseTransform != null)
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