using UnityEngine;

[RequireComponent(typeof(RangedStateManager))]
public class RangedEnemyHealth : HealthSystem
{
    [SerializeField] private GameObject canvasGameObject;

    private RangedStateManager rangedStateManager;
    
    protected override void Awake()
    {
        base.Awake();
        rangedStateManager = GetComponent<RangedStateManager>();
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        OnDead += DeadActions;
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        OnDead -= DeadActions;
    }
    
    private void DeadActions()
    {
        canvasGameObject.SetActive(false);
        rangedStateManager.enabled = false;
    }
}
