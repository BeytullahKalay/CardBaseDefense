using UnityEngine;
using UnityEngine.AI;


public abstract class AnimationControllerSystem : MonoBehaviour
{
    [SerializeField] protected GameObject canvasGameObject;
    
    protected Animator Animator;

    
    protected const string MoveSpeedValName = "MoveSpeed";
    protected const string AttackValName = "Attack";
    protected const string DeadValName = "Dead";
    

    protected NavMeshAgent Agent { get; set; }
    protected HealthSystem HealthSystem { get; set; }
    
    
    public virtual void OnEnable()
    {
        HealthSystem.OnDead += OnDead;
    }



    public virtual void OnDisable()
    {
        HealthSystem.OnDead -= OnDead;
    }
    
    public virtual void Update()
    {
        
        Animator.SetFloat(MoveSpeedValName, Agent.velocity.magnitude);


        //Flip();
    }

    public virtual void OnDead()
    {
        Animator.SetTrigger(DeadValName);
        canvasGameObject.SetActive(false);
    }
    
    
    // using by animation system
    public virtual void CallDestroy()
    {
        HealthSystem.DestroyGameObject();
    }
}
