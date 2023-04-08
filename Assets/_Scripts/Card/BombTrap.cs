using UnityEngine;

public class BombTrap : ActionCard
{
    [SerializeField] private float explosionRadius = 1f;
    [SerializeField] private int explosionDamage = 25;
    [SerializeField] private LayerMask whatIsHittableLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TryGetComponent<CollisionDetectionOnPlacing>(out var onPlacing)) return;
        
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        
        
        var detectedDamageables =
            Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatIsHittableLayer);

        foreach (var damageable in detectedDamageables)
        {
            damageable.GetComponent<HealthSystem>().GetDamage(explosionDamage);
        }
        Destroy(gameObject);
    }
}