using System;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AgentOverride2d))]
public class BaseRusherEnemy : MonoBehaviour
{
    [SerializeField] private float attackDistance;

    [SerializeField] private float explosionRadius;

    [SerializeField] private int explosionDamage = 20;

    [SerializeField] private LayerMask whatIsHurtLayer;

    private Vector2 _destinationPosition;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        var basePosition = (Vector2)GameManager.Instance.BaseTransform.position;
        var direction = ((Vector2)transform.position - basePosition).normalized;
        _destinationPosition = basePosition + direction * attackDistance;
    }

    private void Start()
    {
        _agent.SetDestination(_destinationPosition);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            Debug.Log("BOOOM!!");

            var collider = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatIsHurtLayer);

            if (collider.Length > 0)
            {
                foreach (var collider2D in collider)
                {
                    collider2D.GetComponent<HealthSystem>().TakeDamage?.Invoke(explosionDamage);
                }
            }
            
            Destroy(gameObject);
        }
    }
}