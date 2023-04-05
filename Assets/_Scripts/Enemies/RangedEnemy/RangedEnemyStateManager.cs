using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyStateManager : MonoBehaviour
{
    [SerializeField] private RangedData rangedData;
    private NavMeshAgent _agent;


    private EnemyBaseState _currentState;
    
    public EnemyIdleState EnemyIdleState;
    public EnemyMoveState EnemyMoveState;
    public EnemyAttackState EnemyAttackState;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        EnemyAttackState = new EnemyAttackState(rangedData, transform, _agent,
            GameManager.Instance.BaseTransform.position);
        EnemyIdleState = new EnemyIdleState(transform, _agent);
        EnemyMoveState = new EnemyMoveState(_agent);
        
        _currentState = EnemyIdleState;

    }

    private void Start()
    {
        _currentState.OnEnter(this);
    }

    private void Update()
    {
        _currentState.OnUpdate(this);
    }

    public void SwitchState(EnemyBaseState newState)
    {
        _currentState.OnExit(this);
        _currentState = newState;
        _currentState.OnEnter(this);
    }

    public List<GameObject> DetectTargets()
    {
        var allColliders = Physics2D.OverlapCircleAll(transform.position, rangedData.DetectEnemyRadius,
            rangedData.WhatIsEnemyLayer);
        
        var hittableObjects = new List<GameObject>();

        foreach (var collider in allColliders)
        {
            if (collider.TryGetComponent<CollisionDetectionOnPlacing>(out var hittable))
            {
                continue;
            }
            else
            {
                hittableObjects.Add(collider.gameObject);
            }
        }

        return hittableObjects;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangedData.DetectEnemyRadius);
    }
}