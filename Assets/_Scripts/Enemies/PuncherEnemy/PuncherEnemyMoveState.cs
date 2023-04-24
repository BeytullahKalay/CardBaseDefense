using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuncherEnemyMoveState : PuncherEnemyBaseState
{
    private NavMeshAgent _agent;
    private Transform _transform;
    private float _attackDistance;

    public PuncherEnemyMoveState(NavMeshAgent agent, Transform transform, float attackDistance)
    {
        _agent = agent;
        _transform = transform;
        _attackDistance = attackDistance;
    }

    public override void OnEnter(PuncherStateManager stateManager)
    {
    }

    public override void OnUpdate(PuncherStateManager stateManager)
    {
        var detectedTargets = stateManager.DetectTargets();

        if (detectedTargets.Capacity > 0)
        {
            var closestTarget = FindClosestTarget(detectedTargets);

            if (Vector3.Distance(_transform.position, closestTarget.transform.position) < _attackDistance)
            {
                stateManager.SwitchState(stateManager.PuncherEnemyAttackState);
            }
            else
            {
                _agent.SetDestination(closestTarget.transform.position);
            }
        }
        else if (GameManager.Instance.BaseTransform != null)
        {
            _agent.SetDestination(GameManager.Instance.BaseTransform.position);
        }
        else
        {
            _agent.SetDestination(_transform.position);
            stateManager.SwitchState(stateManager.PuncherEnemyIdleState);
        }
    }

    public override void OnExit(PuncherStateManager stateManager)
    {
    }

    private GameObject FindClosestTarget(List<GameObject> targetList)
    {
        var closest = targetList[0];
        var closestDistance = Vector3.Distance(_transform.position, closest.transform.position);

        foreach (var target in targetList)
        {
            var distanceToTarget = Vector3.Distance(_transform.position, target.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closest = target;
                closestDistance = distanceToTarget;
            }
        }

        return closest;
    }
}