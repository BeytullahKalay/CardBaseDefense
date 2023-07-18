using UnityEngine;

public class BuffBuildingCircleController : OnClicked,IUnSelect
{
    [SerializeField] private Transform circleTransform;


    private BuffBuilding _buffBuilding;
    private CollisionDetectionOnPlacing _collisionDetection;
    private Color _defaultColor;

    private bool _isCollisionDetectionDestroyed;
    private SpriteRenderer _circleRenderer;


    public virtual void Awake()
    {
        _buffBuilding = GetComponent<BuffBuilding>();
        _collisionDetection = GetComponent<CollisionDetectionOnPlacing>();
        _circleRenderer = circleTransform.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _collisionDetection.PlaceAction += CloseCircleRendererAlpha;
        _collisionDetection.PlaceAction += CollisionDetectionDestroyed;
    }

    private void OnDisable()
    {
        _collisionDetection.PlaceAction -= CloseCircleRendererAlpha;
        _collisionDetection.PlaceAction -= CollisionDetectionDestroyed;
    }

    private void Start()
    {
        _defaultColor = _circleRenderer.color;
        circleTransform.localScale = Vector3.one * (_buffBuilding.BuffRadius * 2);
    }

    public virtual void Update()
    {
        if (_isCollisionDetectionDestroyed) return;
        
        if (_collisionDetection.Usable)
            OpenCircleRendererAlpha();
        else
            CloseCircleRendererAlpha();
    }

    private void OpenCircleRendererAlpha()
    {
        _circleRenderer.color = _defaultColor;
    }

    private void CloseCircleRendererAlpha()
    {
        _circleRenderer.SetColorAlpha(0);
    }

    private void CollisionDetectionDestroyed()
    {
        _isCollisionDetectionDestroyed = true;
        CloseCircleRendererAlpha();
    }

    public void UnSelected()
    {
        CloseCircleRendererAlpha();
    }

    public override void OnSelected()
    {
        base.OnSelected();
        OpenCircleRendererAlpha();
    }
}