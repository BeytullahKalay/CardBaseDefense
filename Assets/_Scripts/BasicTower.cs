using UnityEngine;

public class BasicTower : MonoBehaviour
{
    [SerializeField] private TowerData _data;

    private float _nextFireTime = float.MinValue;

    private void Update()
    {
        if (Time.time > _nextFireTime && DetectedEnemies().Length > 0)
        {
            var target = DetectedEnemies()[0];
            var obj =Instantiate(_data.FirePrefab, transform.position, Quaternion.identity);
            obj.GetComponent<FireObject>().Initialize(transform,target.transform,_data.Damage);
            _nextFireTime = Time.time + 1 / _data.FiringFrequency;
        }
    }

    private Collider2D[] DetectedEnemies()
    {
        return Physics2D.OverlapCircleAll(transform.position, _data.DetectEnemyRadius, _data.WhatIsEnemyLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _data.DetectEnemyRadius);
    }
}