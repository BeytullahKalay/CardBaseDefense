using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionOnPlacing : MonoBehaviour,IPlaceable
{
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask whatIsNotPlaceableLayerMask;
    //[HideInInspector] public bool Collide;
    
    public bool Placeable { get; set; }


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

    private void Start()
    {
        _spriteRenderer.sortingOrder += 1;
    }

    private void Update()
    {
        var col = Physics2D.OverlapCircleAll(transform.position, detectRadius, whatIsNotPlaceableLayerMask);
        var colList = new List<Collider2D>(col);
        colList.Remove(_collider);


        var mousePosVector2Int = Vector2Int.RoundToInt(Helpers.GetWorldPositionOfPointer(Helpers.MainCamera));
        var isOnGroundTile = _tilemapManager.GroundTilemap.HasTile(new Vector3Int(mousePosVector2Int.x,mousePosVector2Int.y,0));

        if (colList.Count > 0 || !isOnGroundTile)
        {
            //Collide = true;
            Placeable = false;
            _spriteColor.a = .5f;
            _spriteRenderer.material.color = _spriteColor;
        }
        else
        {
            //Collide = false;
            Placeable = true;
            _spriteColor.a = 1f;
            _spriteRenderer.material.color = _spriteColor;
        }
    }

    public void PlaceActions()
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
        Gizmos.color = !Placeable ? Color.red : Color.green;

        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    private void OnDestroy()
    {
        _spriteRenderer.sortingOrder -= 1;
    }

}