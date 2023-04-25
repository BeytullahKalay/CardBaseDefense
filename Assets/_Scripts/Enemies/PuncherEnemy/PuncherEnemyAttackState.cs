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
    private PunchData _punchData;

    public PuncherEnemyAttackState(Transform transform, NavMeshAgent agent, Vector2 basePosition,
        PunchData punchData)
    {
        _transform = transform;
        _agent = agent;
        _basePosition = basePosition;
        _pooler = Pooler.Instance;
        _punchData = punchData;
    }

    public override void OnEnter(PuncherStateManager stateManager)
    {
    }

    public override void OnUpdate(PuncherStateManager stateManager)
    {
        if (stateManager.DetectTargets().Count > 0)
        {
            if (!(Time.time > _nextFireTime)) return;

            _agent.SetDestination(_transform.position);
            var target = FindClosestTarget(stateManager.DetectTargets());
            var dir = (target.transform.position - _transform.position).normalized;
            var distance = Vector2.Distance(_transform.position, target.transform.position);
            _transform.DOPunchPosition(dir * (distance - .5f), .5f, 0, 0).OnStart(() =>
                {
                    // Task.Factory.StartNew(() =>
                    // {
                    //     System.Threading.Thread.Sleep(250);
                    //     GiveDamage(target);
                    // });
                })
                .OnComplete(() =>
                {
                    GiveDamage(target);
                });

            _nextFireTime = Time.time + 1 / _punchData.PunchFrequency;
        }
        else
        {
            _agent.SetDestination(_basePosition);
            stateManager.SwitchState(stateManager.PuncherEnemyMoveState);
        }
    }

    private void GiveDamage(GameObject target)
    {
        target.GetComponent<HealthSystem>().TakeDamage?.Invoke(_punchData.Damage);
        
        var particleCanvas = _pooler.ParticleTextPool.Get();
        particleCanvas.GetComponent<ParticleCanvas>().PlayTextAnimation(_punchData.Damage.ToString(),
            target.transform.position);
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