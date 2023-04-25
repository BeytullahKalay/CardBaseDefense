using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Punch Data")]
public class PunchData : ScriptableObject
{
   [SerializeField] private float detectRadius;
   [SerializeField] private float punchDistance;
   [SerializeField] Vector2Int damage;
   [SerializeField] private float punchFrequency;
   [SerializeField] private LayerMask whatIsTargetLayer;


   public float DetectRadius => detectRadius;
   public float PunchDistance => punchDistance;
   public float PunchFrequency => punchFrequency;
   
   public int Damage => Random.Range(damage.x, damage.y);

   public LayerMask WhatIsTargetLayer => whatIsTargetLayer;


}
