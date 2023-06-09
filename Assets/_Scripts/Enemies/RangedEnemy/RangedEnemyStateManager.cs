using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyStateManager : MonoBehaviour,IOnBoard,IEnemy
{

    [SerializeField] private RangedData rangedData;
    [HideInInspector]public UnitStates UnitStates;
    
    private RangedEnemyBaseState _currentState;
    public NavMeshAgent NavMeshAgent{ get; set; }
    
    public RangedRangedEnemyIdleState RangedRangedEnemyIdleState;
    public RangedRangedEnemyMoveState RangedRangedEnemyMoveState;
    public RangedEnemyAttackState RangedEnemyAttackState;
    
    
    public BoardStates BoardState { get; set; }
    public Transform BoardedTransform { get; set; }


    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        BoardedTransform = transform;
        
        RangedEnemyAttackState = new RangedEnemyAttackState(rangedData, transform, NavMeshAgent,
            GameManager.Instance.BaseTransform.position);
        RangedRangedEnemyIdleState = new RangedRangedEnemyIdleState();
        RangedRangedEnemyMoveState = new RangedRangedEnemyMoveState(NavMeshAgent,transform);
        
        _currentState = RangedRangedEnemyIdleState;

    }

    private void Start()
    {
        _currentState.OnEnter(this);
    }

    private void Update()
    {
        _currentState.OnUpdate(this);
    }

    public void SwitchState(RangedEnemyBaseState newState)
    {
        _currentState.OnExit(this);
        _currentState = newState;
        _currentState.OnEnter(this);
    }

    public List<GameObject> DetectTargets()
    {
        var allColliders = Physics2D.OverlapCircleAll(transform.position, rangedData.DetectEnemyRadius,
            rangedData.WhatIsTargetLayer);
        
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
    
    public void RemoveFromSpawnerList()
    {
        Spawner.Instance.SpawnedEnemies.Remove(gameObject);
    }
    
    private void OnDestroy()
    {
        RemoveFromSpawnerList();
        CheckIsWaveCleared();
    }

    public void CheckIsWaveCleared()
    {
        EventManager.CheckIsWaveCleared?.Invoke();
    }
}