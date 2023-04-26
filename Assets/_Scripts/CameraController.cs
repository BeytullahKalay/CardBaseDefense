using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private Vector2 minMaxOrthographicSize = new Vector2(10, 15);

    private float _horizontalInput;
    private float _verticalInput;
    private float _mouseWheelInput;
    private float _desOrthographicSize;

    private Vector2 _inputs;
    private Camera _cam;

    [Header("max position values")]
    [SerializeField] private float _maxUpPosition = 5;
    [SerializeField] private float _maxDownPosition = -5;
    [SerializeField] private float _maxRightPosition = 5;
    [SerializeField] private float _maxLeftPosition = -5;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Start()
    {
        _desOrthographicSize = _cam.orthographicSize;
    }


    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _inputs = new Vector2(_horizontalInput, _verticalInput);
        _mouseWheelInput = Input.mouseScrollDelta.y;
    }

    private void FixedUpdate()
    {
        HandleCameraPosition();

        HandleZoomInAndOut();
    }

    private void HandleZoomInAndOut()
    {
        _desOrthographicSize -= _mouseWheelInput * zoomSpeed;

        _desOrthographicSize = Mathf.Clamp(_desOrthographicSize, minMaxOrthographicSize.x, minMaxOrthographicSize.y);

        _cam.orthographicSize =
            Mathf.Lerp(_cam.orthographicSize, _desOrthographicSize, lerpSpeed * Time.fixedDeltaTime);
    }

    private void HandleCameraPosition()
    {
        var desPos = transform.position += (Vector3)_inputs * (Time.fixedDeltaTime * speedMultiplier);
        desPos.x = Mathf.Clamp(desPos.x, _maxLeftPosition, _maxRightPosition);
        desPos.y = Mathf.Clamp(desPos.y, _maxDownPosition, _maxUpPosition);
        transform.position = desPos;
    }

    public void UpdateMaxMovePosition(float startX, float startY, float endX, float endY)
    {
        _maxUpPosition = startY > _maxUpPosition ? startY : _maxUpPosition;
        _maxDownPosition = endY < _maxDownPosition ? endY : _maxDownPosition;

        _maxLeftPosition = startX < _maxLeftPosition ? startX : _maxLeftPosition;
        _maxRightPosition = endX > _maxRightPosition ? endX : _maxRightPosition;
    }
}