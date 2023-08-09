using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollisionDetectionOnPlacing : MonoBehaviour,IPlaceable
{
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask whatIsNotPlaceableLayerMask;

    public Action PlaceAction;
    
    public bool Usable { get; set; }


    private List<IActionCard> _cardActions = new List<IActionCard>();

    private Collider2D _collider;

    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;

    private TilemapManager _tilemapManager;
    
    private MouseStateManager _mouseStateManager;


    private void Awake()
    {
        _tilemapManager = TilemapManager.Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.material.color;
        _collider = GetComponent<Collider2D>();
        _mouseStateManager = MouseStateManager.Instance;
        CloseActionCardScripts();
    }

    private void Start()
    {
        OpenActions();
        _collider.enabled = false;
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
        
        PlayPlacingAnimation();
        
        PlaceAction?.Invoke();

        Destroy(this);
    }

    private void PlayPlacingAnimation()
    {
        transform.DOPunchScale(Vector3.one * .5f, .25f, 2);
    }

    public void OpenActions()
    {
        _spriteRenderer.sortingOrder += 1;
        _mouseStateManager.SetMouseBusyStateTo(MouseState.Busy);
    }

    public void DestroyActions()
    {
        _spriteRenderer.sortingOrder -= 1;
        _mouseStateManager.SetMouseBusyStateTo(MouseState.Available);
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