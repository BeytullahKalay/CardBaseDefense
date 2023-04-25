using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuncherStateManager : MonoBehaviour, IOnBoard,IEnemy
{
    [SerializeField] private PunchData punchData;
    [SerializeField] private BoardStates startState;
    [HideInInspector]public UnitStates UnitStates;

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
        PuncherEnemyMoveState = new PuncherEnemyMoveState(NavMeshAgent, transform, punchData);
        PuncherEnemyAttackState = new PuncherEnemyAttackState(transform, NavMeshAgent,
            GameManager.Instance.BaseTransform.position, punchData);

        _currentState = PuncherEnemyIdleState;
    }

    private void Start()
    {
        _currentState.OnEnter(this);
        BoardState = startState;
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
        var allColliders = Physics2D.OverlapCircleAll(transform.position, punchData.DetectRadius,
            punchData.WhatIsTargetLayer);

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
        Gizmos.DrawWireSphere(transform.position, punchData.DetectRadius);
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