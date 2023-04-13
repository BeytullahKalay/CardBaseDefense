using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class PuncherEnemyAttackState : PuncherEnemyBaseState
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private Vector2 _basePosition;
    private float _nextFireTime = float.MinValue;
    private Pooler _pooler;
    private PunchEnemyData _punchEnemyData;

    public PuncherEnemyAttackState(Transform transform, NavMeshAgent agent, Vector2 basePosition,
        PunchEnemyData punchEnemyData)
    {
        _transform = transform;
        _agent = agent;
        _basePosition = basePosition;
        _pooler = Pooler.Instance;
        _punchEnemyData = punchEnemyData;
    }

    public override void OnEnter(PuncherEnemyStateManager stateManager)
    {
    }

    public override void OnUpdate(PuncherEnemyStateManager stateManager)
    {
        if (stateManager.DetectTargets().Count > 0)
        {
            if (!(Time.time > _nextFireTime)) return;

            _agent.SetDestination(_transform.position);
            var target = FindClosestTarget(stateManager.DetectTargets());
            var dir = (target.transform.position - _transform.position).normalized;
            var distance = Vector2.Distance(_transform.position, target.transform.position);
            _transform.DOPunchPosition(dir * distance, .5f, 0, 0)
                .OnComplete(() =>
                {
                    target.GetComponent<HealthSystem>().GetDamage(_punchEnemyData.Damage);
                    var particleCanvas = _pooler.ParticleTextPool.Get();
                    particleCanvas.GetComponent<ParticleCanvas>().PlayTextAnimation(_punchEnemyData.Damage.ToString(),
                        target.transform.position);
                }).OnStepComplete(() => Debug.Log("step completed"));

            _nextFireTime = Time.time + 1 / _punchEnemyData.PunchFrequency;
        }
        else
        {
            _agent.SetDestination(_basePosition);
            stateManager.SwitchState(stateManager.PuncherEnemyMoveState);
        }
    }

    public override void OnExit(PuncherEnemyStateManager stateManager)
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