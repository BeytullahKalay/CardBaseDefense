using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionOnPlacing : MonoBehaviour,IPlaceable
{
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask whatIsNotPlaceableLayerMask;
    
    public bool Usable { get; set; }


    private List<IActionCard> _cardActions = new List<IActionCard>();

    private Collider2D _collider;

    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;

    private TilemapManager _tilemapManager;

    private void Awake()
    {
        _tilemapManager = TilemapManager.Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.material.color;
        _collider = GetComponent<Collider2D>();
        CloseActionCardScripts();
    }

    private void Start()
    {
        OpenActions();
    }
    
    private void CloseActionCardScripts()
    {
        var actions = GetComponents<IActionCard>();

        foreach (var cardAction in actions)
        {
            cardAction.Enable(false);
            _cardActions.Add(cardAction);
        }
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
            Usable = false;
            _spriteColor.a = .5f;
            _spriteRenderer.material.color = _spriteColor;
        }
        else
        {
            Usable = true;
            _spriteColor.a = 1f;
            _spriteRenderer.material.color = _spriteColor;
        }
    }

    public void PlaceActions()
    {
        OpenActionCardScripts();

        _collider.enabled = true;

        Destroy(this);
    }

    public void SetMouseBusyStateTo(bool state)
    {
        EventManager.SetMouseStateTo?.Invoke(state ? MouseState.Busy : MouseState.Available);
    }

    public void OpenActions()
    {
        _spriteRenderer.sortingOrder += 1;
        SetMouseBusyStateTo(true);
    }

    public void DestroyActions()
    {
        _spriteRenderer.sortingOrder -= 1;
        SetMouseBusyStateTo(false);
    }

    private void OpenActionCardScripts()
    {
        foreach (var cardAction in _cardActions)
        {
            cardAction.Enable(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = !Usable ? Color.red : Color.green;

        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    private void OnDestroy()
    {
        DestroyActions();
    }

}