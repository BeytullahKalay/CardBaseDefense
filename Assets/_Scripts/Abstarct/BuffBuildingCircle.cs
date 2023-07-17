using UnityEngine;

public abstract class BuffBuildingCircle : MonoBehaviour
{
    private BuffBuilding _buffBuilding;
    protected SpriteRenderer Renderer;
    private CollisionDetectionOnPlacing _collisionDetection;
    private Color _defaultColor;

    protected bool IsCollisionDetectionDestroyed;


    public virtual void Awake()
    {
        _buffBuilding = GetComponentInParent<BuffBuilding>();
        _collisionDetection = GetComponentInParent<CollisionDetectionOnPlacing>();

        Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _collisionDetection.PlaceAction += CloseCircle;
        _collisionDetection.PlaceAction += CollisionDetectionDestroyed;
    }

    private void OnDisable()
    {
        _collisionDetection.PlaceAction -= CloseCircle;
        _collisionDetection.PlaceAction -= CollisionDetectionDestroyed;
    }

    public virtual void Start()
    {
        _defaultColor = Renderer.color;
        transform.localScale = Vector3.one * (_buffBuilding.BuffRadius * 2);
    }

    public virtual void Update()
    {
        if (_collisionDetection.Usable && !IsCollisionDetectionDestroyed)
            OpenCircle();
        else
            CloseCircle();
    }

    private void OpenCircle()
    {
        Renderer.color = _defaultColor;
    }

    private void CloseCircle()
    {
        Renderer.SetColorAlpha(0);
    }

    private void CollisionDetectionDestroyed()
    {
        IsCollisionDetectionDestroyed = true;
    }
}