using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PuncherStateManager))]
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
        // if (_puncherStateManager.UnitStates != UnitStates.Attack)
        // {
        //     _spriteRenderer.flipX = Agent.velocity.x <= 0;
        // }
        // else
        // {
        //     var target = _puncherStateManager.DetectTargets()[0];
        //     var dir = (target.transform.position - transform.position).normalized;
        //     _spriteRenderer.flipX = dir.x <= 0;
        // }

        if (_puncherStateManager.UnitStates == UnitStates.Move)
        {
            _spriteRenderer.flipX = Agent.velocity.x <= 0;
        }
        else if (_puncherStateManager.UnitStates == UnitStates.Attack)
        {
            var target = _puncherStateManager.DetectTargets()[0];
            var dir = (target.transform.position - transform.position).normalized;
            _spriteRenderer.flipX = dir.x <= 0;
        }
        else
        {
            if (Mathf.Abs(Agent.velocity.x) > .2f)
            {
                _spriteRenderer.flipX = Agent.velocity.x <= 0;
            }
        }
    }
}