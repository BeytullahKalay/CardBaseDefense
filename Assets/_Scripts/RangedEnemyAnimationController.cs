using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class RangedEnemyAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject canvasGameObject;
    
    private Animator _animator;
    private NavMeshAgent _agent;
    private UnitHealth _healthSystem;
    private SpriteRenderer _spriteRenderer;
    private RangedEnemyStateManager _rangedEnemyStateManager;

    private const string MoveSpeedValName = "MoveSpeed";
    private const string AttackValName = "Attack";
    private const string DeadValName = "Dead";

    private void Awake()
    {
        _healthSystem = GetComponentInParent<UnitHealth>();
        _agent = GetComponentInParent<NavMeshAgent>();
        _rangedEnemyStateManager = GetComponentInParent<RangedEnemyStateManager>();
        
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _healthSystem.OnDead += OnDead;
    }
    
    private void OnDisable()
    {
        _healthSystem.OnDead -= OnDead;
    }

    private void Update()
    {
        _animator.SetFloat(MoveSpeedValName,_agent.velocity.magnitude);
        
        _spriteRenderer.flipX = _agent.velocity.x <= 0;
        
    }

    private void OnDead()
    {
        _rangedEnemyStateManager.enabled = false;
        _animator.SetTrigger(DeadValName);
        canvasGameObject.SetActive(false);
    }

    // using by animation system
    public void CallDestroy()
    {
        _healthSystem.DestroyGameObject();
    }
}
