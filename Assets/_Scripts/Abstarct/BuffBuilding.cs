

using UnityEngine;

public abstract class BuffBuilding: MonoBehaviour
{
   [SerializeField] protected float buffRadius = 3f;
   
   public float BuffRadius => buffRadius;
   
   private void OnDrawGizmos()
   {
      Gizmos.DrawWireSphere(transform.position, buffRadius);
   }

}
