using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    private RangedData _rangedData;
    private Transform _transform;
    private NavMeshAgent _agent;
    private Vector2 _basePosition;
    private float _nextFireTime = float.MinValue;

    public EnemyAttackState(RangedData rangedData,Transform transform,NavMeshAgent agent,Vector2 basePosition)
    {
        _rangedData = rangedData;
        _transform = transform;
        _agent = agent;
        _basePosition = basePosition;
    }
    
    public override void OnEnter(RangedEnemyStateManager stateManager)
    {
        Debug.Log("Enter Attack State");
    }

    public override void OnUpdate(RangedEnemyStateManager stateManager)
    {
        if (stateManager.DetectTargets().Capacity > 0)
        {
            if (!(Time.time > _nextFireTime)) return;
                
            _agent.SetDestination(_transform.position);
            var target = stateManager.DetectTargets()[0];
            var obj = stateManager.CreateBullet();
            obj.GetComponent<FireObject>().Initialize(_transform, target.transform, _rangedData.Damage);
            _nextFireTime = Time.time + 1 / _rangedData.FiringFrequency;

        }
        else
        {
            _agent.SetDestination(_basePosition);
            stateManager.SwitchState(stateManager.EnemyMoveState);
        }
    }

    public override void OnExit(RangedEnemyStateManager stateManager)
    {
    }
}
