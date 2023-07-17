using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthSystem : MonoBehaviour, IHealthSystem
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected Slider slider;

    public int Health { get; private set; }

    public Action<int> TakeDamage;
    public Action UpdateSlider;
    public Action OnDead;

    protected Collider2D coll;

    private bool _isDead;

    public int MaxHealth => maxHealth;

    protected virtual void OnEnable()
    {
        TakeDamage += GetDamage;
        OnDead += Die;
        UpdateSlider += SetSlider;
    }

    protected virtual void OnDisable()
    {
        TakeDamage -= GetDamage;
        OnDead -= Die;
        UpdateSlider -= SetSlider;
    }

    protected virtual void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    public virtual void Start()
    {
        Health = maxHealth;
        Initialize();
    }

    private void Initialize()
    {
        slider.value = Health / maxHealth;
        slider.gameObject.SetActive(false);
    }

    public virtual void GetDamage(int damage)
    {
        DecreaseHealth(damage);
        UpdateSlider?.Invoke();
        if (Health <= 0 && !_isDead) OnDead.Invoke();
    }

    private void SetSlider()
    {
        slider.value = (float)Health / maxHealth;
        slider.gameObject.SetActive(true);
    }

    private void DecreaseHealth(int damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, maxHealth);
    }

    public virtual void Die()
    {
        _isDead = true;
        coll.enabled = false;
        slider.gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    
    public virtual void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public virtual void Heal(int healAmount)
    {
        Health += healAmount;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        slider.value = (float)Health / maxHealth;
    }
}