using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Trap Data")]
public class TrapData : ScriptableObject
{
    [SerializeField] private Vector2Int trapDamage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask whatIsHittableLayer;


    public int TrapDamage => Random.Range(trapDamage.x, trapDamage.y);
    public float ExplosionRadius => explosionRadius;
    public LayerMask WhatIsHittableLayer => whatIsHittableLayer;
}
