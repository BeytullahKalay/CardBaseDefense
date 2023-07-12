using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAnimationController : AnimationControllerSystem
{
    private SpriteRenderer _spriteRenderer;
    private RangedStateManager rangedStateManager;
    private RangedAttackState _attackState;

    public virtual void Awake()
    {
        HealthSystem = GetComponentInParent<HealthSystem>();
        Agent = GetComponentInParent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        
        rangedStateManager = GetComponentInParent<RangedStateManager>();
        _attackState = rangedStateManager.RangedAttackState;

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _attackState.Attack += PlayAttackAnimation;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _attackState.Attack -= PlayAttackAnimation;
    }

    public override void Update()
    {
        base.Update();
        Flip();
    }

    private void Flip()
    {
        if (rangedStateManager.UnitStates != UnitStates.Attack)
        {
            _spriteRenderer.flipX = Agent.velocity.x <= 0;
        }
        else
        {
            var target = rangedStateManager.DetectTargets()[0];
            var dir = (target.transform.position - transform.position).normalized;
            _spriteRenderer.flipX = dir.x <= 0;
        }
    }

    public override void OnDead()
    {
        base.OnDead();
        rangedStateManager.enabled = false;
        Animator.SetTrigger(DeadValName);
        canvasGameObject.SetActive(false);
    }

    private void PlayAttackAnimation()
    {
        Animator.SetTrigger(AttackValName);
    }

    // using by animation system
    public void FireBullet()
    {
        _attackState.FireBullet(rangedStateManager);
    }
}