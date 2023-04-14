using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuncherEnemyStateManager : MonoBehaviour, IOnBoard,IEnemy
{
    [SerializeField] private PunchEnemyData punchEnemyData;

    private PuncherEnemyBaseState _currentState;

    public PuncherEnemyIdleState PuncherEnemyIdleState;
    public PuncherEnemyMoveState PuncherEnemyMoveState;
    public PuncherEnemyAttackState PuncherEnemyAttackState;


    public BoardStates BoardState { get; set; }
    public Transform BoardedTransform { get; set; }
    public NavMeshAgent NavMeshAgent { get; set; }

    private void Awake()
    {
        BoardedTransform = transform;
        NavMeshAgent = GetComponent<NavMeshAgent>();


        PuncherEnemyIdleState = new PuncherEnemyIdleState();
        PuncherEnemyMoveState = new PuncherEnemyMoveState(NavMeshAgent, transform, punchEnemyData.PunchDistance);
        PuncherEnemyAttackState = new PuncherEnemyAttackState(transform, NavMeshAgent,
            GameManager.Instance.BaseTransform.position, punchEnemyData);

        _currentState = PuncherEnemyIdleState;
    }

    private void Start()
    {
        _currentState.OnEnter(this);
    }

    private void Update()
    {
        _currentState.OnUpdate(this);
    }

    public void SwitchState(PuncherEnemyBaseState newState)
    {
        _currentState.OnExit(this);
        _currentState = newState;
        _currentState.OnEnter(this);
    }


    public List<GameObject> DetectTargets()
    {
        var allColliders = Physics2D.OverlapCircleAll(transform.position, punchEnemyData.DetectRadius,
            punchEnemyData.WhatIsTargetLayer);

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
        Gizmos.DrawWireSphere(transform.position, punchEnemyData.DetectRadius);
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