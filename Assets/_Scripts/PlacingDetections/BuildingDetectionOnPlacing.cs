using UnityEngine;

public class BuildingDetectionOnPlacing : MonoBehaviour, IPlaceable
{
    [SerializeField] private LayerMask whatIsNotBuildLayerMask;
    
    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;
    

    public bool Usable { get; set; }

    private GameObject _raycastedObject;
    private IBuildEffectCard _effectCard;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.material.color;
        _effectCard = GetComponent<IBuildEffectCard>();
    }

    private void Start()
    {
        OpenActions();
    }

    private void Update()
    {
        var ray = Helpers.MainCamera.ScreenPointToRay((Vector2)Input.mousePosition);
        var hit2D = Physics2D.GetRayIntersection(ray,whatIsNotBuildLayerMask);

        
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

    private void OnDestroy()
    {
        DestroyActions();
    }
}