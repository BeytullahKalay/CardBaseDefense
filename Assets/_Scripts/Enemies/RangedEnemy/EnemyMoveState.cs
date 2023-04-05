

using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : EnemyBaseState
{
    private NavMeshAgent _agent;

    public EnemyMoveState(NavMeshAgent agent)
    {
        _agent = agent;
    }
    
    public override void OnEnter(RangedEnemyStateManager stateManager)
    {
        Debug.Log("Enter Move State");
    }

    public override void OnUpdate(RangedEnemyStateManager stateManager)
    {
        if (stateManager.DetectTargets().Capacity > 0)
        {
            stateManager.SwitchState(stateManager.EnemyAttackState);
        }
        else if (GameManager.Instance.BaseTransform != null)
        {
            _agent.SetDestination(GameManager.Instance.BaseTransform.position);
        }
        else
        {
            stateManager.SwitchState(stateManager.EnemyIdleState);
        }
    }

    public override void OnExit(RangedEnemyStateManager stateManager)
    {
    }
    
    
}
