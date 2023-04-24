using UnityEngine;

[RequireComponent(typeof(RangedEnemyStateManager))]
public class RangedEnemyHealth : HealthSystem
{
    [SerializeField] private GameObject canvasGameObject;

    private RangedEnemyStateManager _rangedEnemyStateManager;
    
    protected override void Awake()
    {
        base.Awake();
        _rangedEnemyStateManager = GetComponent<RangedEnemyStateManager>();
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
        _rangedEnemyStateManager.enabled = false;
    }
}
