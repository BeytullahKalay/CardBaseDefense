using System;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AgentOverride2d))]
public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private RangedData _rangedData;

    private float _nextFireTime = float.MinValue;

    private NavMeshAgent _agent;

    private Vector2 _baseTransform;

    private void Awake()
    {
        _baseTransform = GameManager.Instance.BaseTransform.position;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (DetectTargets().Length > 0)
        {
            if (Time.time > _nextFireTime)
            {
                _agent.SetDestination(transform.position);
                var target = DetectTargets()[0];
                var obj = Instantiate(_rangedData.FirePrefab, transform.position, Quaternion.identity);
                obj.GetComponent<FireObject>().Initialize(transform, target.transform, _rangedData.Damage);
                _nextFireTime = Time.time + 1 / _rangedData.FiringFrequency;
            }
        }
        else
        {
            _agent.SetDestination(_baseTransform);
        }
    }

    private Collider2D[] DetectTargets()
    {
        return Physics2D.OverlapCircleAll(transform.position, _rangedData.DetectEnemyRadius,
            _rangedData.WhatIsEnemyLayer);
    }

    private void OnDrawGizmos()
    {
        //if (enabled) return;
        Gizmos.DrawWireSphere(transform.position, _rangedData.DetectEnemyRadius);
    }
}