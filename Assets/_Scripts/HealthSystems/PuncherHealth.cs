using UnityEngine;


[RequireComponent(typeof(PuncherStateManager))]

public class PuncherHealth : HealthSystem
{
    [SerializeField] private GameObject canvasGameObject;

    private PuncherStateManager _puncherStateManager;
    
    
    protected override void Awake()
    {
        base.Awake();
        _puncherStateManager = GetComponent<PuncherStateManager>();
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
        _puncherStateManager.enabled = false;
        print("puncher dead");
    }
}
