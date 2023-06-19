using UnityEngine;

public class BuildingDetectionOnPlacing : MonoBehaviour, IPlaceable
{
    [SerializeField] private LayerMask whatIsNotBuildLayerMask;
    
    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;
    

    public bool Usable { get; set; }


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.material.color;
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
        }
    }

    private void Update()
    {
        var ray = Helpers.MainCamera.ScreenPointToRay((Vector2)Input.mousePosition);
        var hit2D = Physics2D.GetRayIntersection(ray,whatIsNotBuildLayerMask);

        if (hit2D.collider != null && hit2D.transform.TryGetComponent<HealthSystem>(out var healthSystem))
        {
            // repairable
            Usable = true;
            _spriteColor.a = 1f;
            _spriteRenderer.material.color = _spriteColor;
        }
        else
        {
            Usable = false;
            _spriteColor.a = .5f;
            _spriteRenderer.material.color = _spriteColor; 
        }

    }

    public void PlaceActions()
    {
        foreach (var effectCard in GetComponents<IEffectCard>())
        {
            effectCard.DoEffect();
        }
        
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