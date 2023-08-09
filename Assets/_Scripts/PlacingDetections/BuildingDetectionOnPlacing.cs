using UnityEngine;

public class BuildingDetectionOnPlacing : MonoBehaviour, IPlaceable
{
    [SerializeField] private LayerMask whatIsBuildLayerMask;
    
    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;
    
    public bool Usable { get; set; }

    private GameObject _raycastedObject;
    private IBuildingEffectCard _effectCard;
    private MouseStateManager _mouseStateManager;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.material.color;
        _effectCard = GetComponent<IBuildingEffectCard>();
        _mouseStateManager = MouseStateManager.Instance;
    }

    private void Start()
    {
        OpenActions();
    }

    private void Update()
    {
        var ray = Helpers.MainCamera.ScreenPointToRay((Vector2)Input.mousePosition);
        var hit2D = Physics2D.GetRayIntersection(ray,20,whatIsBuildLayerMask);
        
        if (hit2D.collider != null && _effectCard.IsPlaceable(hit2D.collider.gameObject))
        {
            Usable = true;
            SetSpriteAlphaToUsable();
            _raycastedObject = hit2D.collider.gameObject;
        }
        else
        {
            Usable = false;
            SetSpriteAlphaNotUsable();
            _raycastedObject = null;
        }
    }

    private void SetSpriteAlphaNotUsable()
    {
        _spriteColor.a = .5f;
        _spriteRenderer.material.color = _spriteColor;
    }

    private void SetSpriteAlphaToUsable()
    {
        _spriteColor.a = 1f;
        _spriteRenderer.material.color = _spriteColor;
    }

    public void PlaceActions()
    {
        _effectCard.DoEffect(_raycastedObject);
        Destroy(this);
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

    private void OnDestroy()
    {
        DestroyActions();
    }
}