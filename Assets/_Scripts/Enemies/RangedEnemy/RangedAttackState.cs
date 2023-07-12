using System;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackState : RangedBaseState
{
    private RangedData _rangedData;
    private Transform _transform;
    private NavMeshAgent _agent;
    private Vector2 _basePosition;
    private float _nextFireTime = float.MinValue;
    private Pooler _pooler;

    public Action Attack;
    

    public RangedAttackState(RangedData rangedData, Transform transform, NavMeshAgent agent,
        Vector2 basePosition)
    {
        _rangedData = rangedData;
        _transform = transform;
        _agent = agent;
        _basePosition = basePosition;
        _pooler = Pooler.Instance;
    }

    public override void OnEnter(RangedStateManager stateManager)
    {
        stateManager.UnitStates = UnitStates.Attack;
    }

    public override void OnUpdate(RangedStateManager stateManager)
    {
        if (stateManager.DetectTargets().Capacity > 0)
        {
            if (!(Time.time > _nextFireTime) || stateManager.BoardState != BoardStates.Landed) return;

            _agent.SetDestination(_transform.position);
            _nextFireTime = Time.time + 1 / _rangedData.FiringFrequency;
            Attack?.Invoke();
            FireBullet(stateManager);
            //Fire(stateManager);
        }
        else
        {
            _agent.SetDestination(_basePosition);
            stateManager.SwitchState(stateManager.RangedMoveState);
        }
    }

    public void FireBullet(RangedStateManager stateManager)
    {
        var target = stateManager.DetectTargets()[0];
        var obj = _pooler.BulletPool.Get();
        obj.GetComponent<FireObject>().Initialize(_transform, target.transform, _rangedData.Damage);
    }

    // private void Fire(RangedStateManager stateManager)
    // {
    //     FireBullet(stateManager); // <- Bad
    // }

    public override void OnExit(RangedStateManager stateManager)
    {
    }
}