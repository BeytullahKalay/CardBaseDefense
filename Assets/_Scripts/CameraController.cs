using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private Vector2 minMaxOrthographicSize = new Vector2(10, 15);

    private float _horizontalInput;
    private float _verticalInput;
    private Vector2 _inputs;
    private float _mouseWheelInput;
    private Camera _cam;
    private float _desOrthographicSize;

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

        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _desOrthographicSize, lerpSpeed * Time.fixedDeltaTime);
    }

    private void HandleCameraPosition()
    {
        transform.position += (Vector3)_inputs * (Time.fixedDeltaTime * speedMultiplier);
    }
}