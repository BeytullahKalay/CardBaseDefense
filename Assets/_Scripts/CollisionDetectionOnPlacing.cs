using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionOnPlacing : MonoBehaviour
{
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask whatIsNotPlaceableLayerMask;
    [HideInInspector] public bool Collide;

    private List<ActionCard> _cardActions = new List<ActionCard>();

    private Collider2D _collider;

    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;

    private TilemapManager _tilemapManager;

    private void Awake()
    {
        _tilemapManager = TilemapManager.Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.material.color;


        var actions = GetComponents<ActionCard>();

        foreach (var cardAction in actions)
        {
            cardAction.Enable(false);
            _cardActions.Add(cardAction);
        }

        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        var col = Physics2D.OverlapCircleAll(transform.position, detectRadius, whatIsNotPlaceableLayerMask);
        var mousePosVector2Int = Vector2Int.RoundToInt(Helpers.GetWorldPositionOfPointer(Helpers.MainCamera));
        var isOnWater = _tilemapManager.WaterTilemap.HasTile(new Vector3Int(mousePosVector2Int.x,mousePosVector2Int.y,0));

        
        if (col.Length > 1 || isOnWater)
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
        foreach (var cardAciton in _cardActions)
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