using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthSystem : MonoBehaviour, IHealthSystem
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] private Slider slider;

    public int Health { get; private set; }

    public Action<int> TakeDamage;
    public Action OnDead;

    protected Collider2D coll;

    private bool _isDead;

    protected virtual void OnEnable()
    {
        TakeDamage += GetDamage;
        OnDead += Die;
    }

    protected virtual void OnDisable()
    {
        TakeDamage -= GetDamage;
        OnDead -= Die;
    }

    protected virtual void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    public virtual void Start()
    {
        Health = maxHealth;
        slider.value = Health / maxHealth;
        slider.gameObject.SetActive(false);
    }

    public virtual void GetDamage(int damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        slider.value = (float)Health / maxHealth;
        slider.gameObject.SetActive(true);
        if (Health <= 0 && !_isDead) OnDead.Invoke();
    }

    public virtual void Die()
    {
        _isDead = true;
        coll.enabled = false;
        slider.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public virtual void Heal(int healAmount)
    {
        Health += healAmount;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        slider.value = (float)Health / maxHealth;
    }

    public virtual void Knockback(Transform attackTransform, float knocbackForce)
    {
        var dir = (transform.position - attackTransform.position).normalized;
    }
}