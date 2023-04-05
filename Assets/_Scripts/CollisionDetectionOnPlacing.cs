using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionOnPlacing : MonoBehaviour
{
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask whatIsBuilding;
    [HideInInspector] public bool Collide;

    private List<ActionCard> _cardAcitons = new List<ActionCard>();

    private Collider2D _collider;

    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.material.color;


        var actions = GetComponents<ActionCard>();

        foreach (var cardAction in actions)
        {
            cardAction.Enable(false);
            _cardAcitons.Add(cardAction);
        }

        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        var col = Physics2D.OverlapCircleAll(transform.position, detectRadius, whatIsBuilding);
        if (col.Length > 1)
        {
            Collide = true;
            _spriteColor.a = .5f;
            _spriteRenderer.material.color = _spriteColor;
        }
        else
        {
            Collide = false;
            _spriteColor.a = 1f;
            _spriteRenderer.material.color = _spriteColor;
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