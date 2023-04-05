using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Tower Data")]
public class RangedData : ScriptableObject
{
    [SerializeField] private Vector2Int damage = new Vector2Int(8,12);
    [SerializeField] private float detectEnemyRadius = 5f;
    [SerializeField] private float firingFrequency = 1;
    [SerializeField] private LayerMask whatIsEnemyLayer;
    [SerializeField] private GameObject firePrefab;

    public int Damage => Random.Range(damage.x,damage.y);

    public float DetectEnemyRadius => detectEnemyRadius;
    public float FiringFrequency => firingFrequency;

    public LayerMask WhatIsEnemyLayer => whatIsEnemyLayer;

    public GameObject FirePrefab => firePrefab;
}
