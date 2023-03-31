using UnityEngine;

public class CollisionDetectionOnPlacing : MonoBehaviour
{
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask whatIsBuilding;
    public bool Collide;

    private void Update()
    {
        var col = Physics2D.OverlapCircleAll(transform.position,detectRadius,whatIsBuilding);
        if (col.Length > 1)
        {
            Collide = true;
        }
        else
        {
            Collide = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
