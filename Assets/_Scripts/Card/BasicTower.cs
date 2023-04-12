using UnityEngine;

public class BasicTower : ActionCard
{
    [SerializeField] private RangedData _data;

    private float _nextFireTime = float.MinValue;

    private void Update()
    {
        if (Time.time > _nextFireTime && DetectedEnemies().Length > 0)
        {
            var target = DetectedEnemies()[0];
            var bullet = Pooler.Instance.BulletPool.Get();
            bullet.GetComponent<FireObject>().Initialize(transform, target.transform, _data.Damage);
            _nextFireTime = Time.time + 1 / _data.FiringFrequency;
        }
    }

    private Collider2D[] DetectedEnemies()
    {
        return Physics2D.OverlapCircleAll(transform.position, _data.DetectEnemyRadius, _data.WhatIsTargetLayer);
    }

    private void OnDrawGizmos()
    {
        if (enabled) return;
        Gizmos.DrawWireSphere(transform.position, _data.DetectEnemyRadius);
    }
}