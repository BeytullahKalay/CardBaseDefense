using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AgentOverride2d))]
public class BaseRusherEnemy : MonoBehaviour
{
    [SerializeField] private float attackDistance;

    private Vector2 _destinationPosition;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        //_agent.stoppingDistance = attackDistance;
        
        var basePosition = (Vector2)GameManager.Instance.BaseTransform.position;
        var direction = ((Vector2)transform.position - basePosition).normalized;
        _destinationPosition = basePosition + direction * attackDistance;
    }

    private void Start()
    {
        _agent.SetDestination(_destinationPosition);
    }
}