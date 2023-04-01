using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionOnPlacing : MonoBehaviour
{
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask whatIsBuilding;
    [HideInInspector]public bool Collide;

    private List<ActionCard> _cardAcitons = new List<ActionCard>();

    private void Awake()
    {
        var actions = GetComponents<ActionCard>();

        foreach (var cardAction in actions)
        {
            cardAction.Enable(false);
            _cardAcitons.Add(cardAction);
        }
    }

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

    public void OpenActionsAndDestroyCollisionDetection()
    {
        foreach (var cardAciton in _cardAcitons)
        {
            cardAciton.Enable(true);
        }
        Destroy(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Collide ? Color.red : Color.green;

        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
