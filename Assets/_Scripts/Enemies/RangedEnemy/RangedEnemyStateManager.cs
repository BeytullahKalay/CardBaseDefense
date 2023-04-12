using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyStateManager : MonoBehaviour,IOnBoard
{
    [SerializeField] private RangedData rangedData;
    
    private EnemyBaseState _currentState;
    public NavMeshAgent NavMeshAgent{ get; set; }
    
    public EnemyIdleState EnemyIdleState;
    public EnemyMoveState EnemyMoveState;
    public EnemyAttackState EnemyAttackState;
    
    
    public BoardStates BoardState { get; set; }
    public Transform BoardedTransform { get; set; }


    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        BoardedTransform = transform;
        
        EnemyAttackState = new EnemyAttackState(rangedData, transform, NavMeshAgent,
            GameManager.Instance.BaseTransform.position);
        
        var boardStates = BoardState;
        EnemyIdleState = new EnemyIdleState(transform, NavMeshAgent, ref boardStates);
        EnemyMoveState = new EnemyMoveState(NavMeshAgent);
        
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