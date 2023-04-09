using System;
using UnityEngine;

public class BombTrap : ActionCard
{
    // [SerializeField] private float explosionRadius = 1f;
    // [SerializeField] private int explosionDamage = 25;
    // [SerializeField] private LayerMask whatIsHittableLayer;

    [SerializeField] private TrapData _trapData;
    
    [SerializeField] private Color ready;
    [SerializeField] private Color exploded;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
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
        _collider.enabled = false;
        _spriteRenderer.color = exploded;
        _spriteRenderer.SetColorAlpha(.5f);
    }
}