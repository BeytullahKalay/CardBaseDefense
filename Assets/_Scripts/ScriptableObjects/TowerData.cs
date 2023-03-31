using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Tower Data")]
public class TowerData : ScriptableObject
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float detectEnemyRadius = 5f;
    [SerializeField] private float firingFrequency = 1;
    [SerializeField] private LayerMask whatIsEnemyLayer;
    [SerializeField] private GameObject firePrefab;

    public int Damage => damage;

    public float DetectEnemyRadius => detectEnemyRadius;
    public float FiringFrequency => firingFrequency;

    public LayerMask WhatIsEnemyLayer => whatIsEnemyLayer;

    public GameObject FirePrefab => firePrefab;
}
