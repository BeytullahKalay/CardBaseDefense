using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedStateManager : MonoBehaviour,IOnBoard,IEnemy,IUnit
{

    [SerializeField] private RangedData rangedData;
    
    [field: SerializeField]public UnitStates UnitStates { get; set; }
    [field: SerializeField]public BoardStates BoardState { get; set; }
    public Vector2 MovePos { get; set; }

    
    public NavMeshAgent NavMeshAgent{ get; set; }
    
    private RangedBaseState _currentState;
    public RangedIdleState RangedIdleState;
    public RangedMoveState RangedMoveState;
    public RangedAttackState RangedAttackState;
    
    
    public Transform BoardedTransform { get; set; }

    private HealthSystem _health;


    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        BoardedTransform = transform;
        MovePos = GameManager.Instance.BaseTransform.position;

        
        RangedAttackState = new RangedAttackState(rangedData, transform, NavMeshAgent,
            GameManager.Instance.BaseTransform.position);
        RangedIdleState = new RangedIdleState();
        RangedMoveState = new RangedMoveState(NavMeshAgent,transform);
        
        _currentState = RangedIdleState;
        _health = GetComponent<HealthSystem>();

    }

    private void Start()
    {
        _currentState.OnEnter(this);
    }

    private void Update()
    {
        _currentState.OnUpdate(this);
    }

    public void SwitchState(RangedBaseState newState)
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

    public bool IsDead()
    {
        return _health.Health <= 0;
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