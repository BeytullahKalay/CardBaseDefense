using UnityEngine;

public class BombTrap : MonoBehaviour, IActionCard, ITrap
{
    [SerializeField] private TrapData _trapData;
    [SerializeField] private Color ready;
    [SerializeField] private Color exploded;

    public TrapRepair TrapRepair { get; set; }


    private SpriteRenderer _spriteRenderer;
    private GoldManager _goldManager;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _goldManager = GoldManager.Instance;

        SetUpTrap();
    }

    private void SetUpTrap()
    {
        TrapRepair = gameObject.AddComponent<TrapRepair>();
        TrapRepair.TrapData = _trapData;
        TrapRepair.RepairStuff += SetUpBombStuff;
    }

    private void Start()
    {
        _spriteRenderer.color = ready;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.Has<CollisionDetectionOnPlacing>()) return;

        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        var detectedDamageables =
            Physics2D.OverlapCircleAll(transform.position, _trapData.ExplosionRadius, _trapData.WhatIsHittableLayer);

        foreach (var damageable in detectedDamageables)
        {
            damageable.GetComponent<HealthSystem>().GetDamage(_trapData.TrapDamage);
        }

        ExplosionStuff();
    }

    private void ExplosionStuff()
    {
        _spriteRenderer.color = exploded;
        _spriteRenderer.SetColorAlpha(.5f);
    }

    private void SetUpBombStuff()
    {
        if (!_goldManager.IsPurchasable(_trapData.RepairCost)) return;
        _spriteRenderer.color = ready;
        _spriteRenderer.SetColorAlpha(1f);
        EventManager.AddThatToCurrentGold?.Invoke(-_trapData.RepairCost);
    }

    public void Enable(bool state)
    {
        this.enabled = state;
    }
}