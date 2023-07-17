using System;
using UnityEngine;

[RequireComponent(typeof(BasicTower))]
public class BaseHealth : HealthSystem,IHasHealthbarSlider
{
    [Space(10)]
    
    [SerializeField] private GameObject canvasGameObject;
    
    public Action OnHeal;
    
    private BasicTower _basicTowerScript;
    

    protected override void Awake()
    {
        base.Awake();
        _basicTowerScript = GetComponent<BasicTower>();
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
        _basicTowerScript.enabled = false;
        EventManager.GameOver?.Invoke();
    }

    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        OnHeal?.Invoke();
        UpdateSlider?.Invoke();
    }

    public GameObject GetHealthBar()
    {
        return slider.gameObject;
    }
}
