using UnityEngine;

public class BuffBuildingCircleController : BuffBuildingCircle
{
    [SerializeField] private float borderDistance = 5;

    private Camera _camera;

    private float _colorAlpha;

    public override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    public override void Start()
    {
        base.Start();
        _colorAlpha = Renderer.color.a;
    }

    private void LateUpdate()
    {
        if (!IsCollisionDetectionDestroyed) return;

        var mousePos = Helpers.GetWorldPositionOfPointer(_camera);
        var distanceToTransform = Vector2.Distance(transform.position, mousePos);

        if (distanceToTransform > borderDistance) return;

        var remappedValue = distanceToTransform.Remap(0, borderDistance, 0, _colorAlpha);
        Renderer.SetColorAlpha(_colorAlpha - remappedValue);
    }
}