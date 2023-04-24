using UnityEngine;
using UnityEngine.AI;

public class PuncherAnimationController : AnimationControllerSystem
{
    private SpriteRenderer _spriteRenderer;
    private PuncherStateManager _puncherStateManager;

    public virtual void Awake()
    {
        HealthSystem = GetComponent<HealthSystem>();
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _puncherStateManager = GetComponent<PuncherStateManager>();
    }

    public override void Update()
    {
        base.Update();
        Flip();
    }
    
    private void Flip()
    {
        if (_puncherStateManager.UnitStates != UnitStates.Attack)
        {
            _spriteRenderer.flipX = Agent.velocity.x <= 0;
        }
        else
        {
            var target = _puncherStateManager.DetectTargets()[0];
            var dir = (target.transform.position - transform.position).normalized;
            _spriteRenderer.flipX = dir.x <= 0;
        }
    }
}
