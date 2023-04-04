using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionOnPlacing : MonoBehaviour
{
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask whatIsBuilding;
    [HideInInspector]public bool Collide;

    private List<ActionCard> _cardAcitons = new List<ActionCard>();

    private Collider2D _collider;

    private void Awake()
    {
        var actions = GetComponents<ActionCard>();

        foreach (var cardAction in actions)
        {
            cardAction.Enable(false);
            _cardAcitons.Add(cardAction);
        }

        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
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

        _collider.enabled = true;
        
        Destroy(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Collide ? Color.red : Color.green;

        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
