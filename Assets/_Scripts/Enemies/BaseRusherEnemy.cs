using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AgentOverride2d))]
public class BaseRusherEnemy : MonoBehaviour,IOnBoard
{
    [SerializeField] private float attackDistance;

    [SerializeField] private float explosionRadius;

    [SerializeField] private float explosionTextFadeDuration = 2f;

    [SerializeField] private int explosionDamage = 20;
    
    [SerializeField] private LayerMask whatIsHurtLayer;


    private Vector2 _destinationPosition;


    private Pooler _pooler;
    
    public BoardStates BoardState { get; set; }
    public Transform BoardedTransform { get; set; }
    public NavMeshAgent NavMeshAgent{ get; set; }


    private void Awake()
    {
        BoardedTransform = transform;
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _pooler = Pooler.Instance;

        var basePosition = (Vector2)GameManager.Instance.BaseTransform.position;
        var direction = ((Vector2)transform.position - basePosition).normalized;
        _destinationPosition = basePosition + direction * attackDistance;
    }

    private void Update()
    {
        if(BoardState != BoardStates.Landed) return;
        NavMeshAgent.SetDestination(_destinationPosition);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            var collider = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatIsHurtLayer);

            if (collider.Length > 0)
            {
                foreach (var collider2D in collider)
                {
                    collider2D.GetComponent<HealthSystem>().TakeDamage?.Invoke(explosionDamage);
                    var particleCanvas = _pooler.ParticleTextPool.Get();
                    particleCanvas.GetComponent<ParticleCanvas>().PlayTextAnimation(explosionDamage.ToString(),
                        collider2D.transform.position,explosionTextFadeDuration);
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }


}