using UnityEngine;

public class BasicTower : MonoBehaviour, IActionCard
{
    [SerializeField] private RangedData _data;

    private float _nextFireTime = float.MinValue;

    private void Update()
    {
        if (Time.time > _nextFireTime && DetectedEnemies().Length > 0)
        {
            SpawnBulletAndInitializeNextShootTime();

            // play shoot soundFX
            SoundFXManager.Instance.PlaySoundFXClip(_data.ShootClip,transform);
        }
    }

    private void SpawnBulletAndInitializeNextShootTime()
    {
        var target = DetectedEnemies()[0];
        var bullet = Pooler.Instance.BulletPool.Get();

        if (bullet.TryGetComponent<FireObject>(out var fireObject))
        {
            fireObject.Initialize(transform, target.transform, _data.Damage);
            _nextFireTime = Time.time + 1 / _data.FiringFrequency;
        }
        else
        {
            Debug.LogError("No FireObject script in " + bullet.name);
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

    public void Enable(bool state)
    {
        this.enabled = state;
    }


}