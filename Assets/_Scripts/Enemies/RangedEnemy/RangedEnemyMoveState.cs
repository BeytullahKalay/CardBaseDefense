using UnityEngine.AI;

public class RangedEnemyMoveState : EnemyBaseState
{
    private NavMeshAgent _agent;

    public RangedEnemyMoveState(NavMeshAgent agent)
    {
        _agent = agent;
    }
    
    public override void OnEnter(RangedEnemyStateManager stateManager)
    {
    }

    public override void OnUpdate(RangedEnemyStateManager stateManager)
    {
        if (stateManager.DetectTargets().Capacity > 0)
        {
            stateManager.SwitchState(stateManager.RangedEnemyAttackState);
        }
        else if (stateManager.BoardState == BoardStates.Landed && GameManager.Instance.BaseTransform != null)
        {
            _agent.SetDestination(GameManager.Instance.BaseTransform.position);
        }
        else
        {
            stateManager.SwitchState(stateManager.RangedEnemyIdleState);
        }
    }

    public override void OnExit(RangedEnemyStateManager stateManager)
    {
    }
    
    
}
