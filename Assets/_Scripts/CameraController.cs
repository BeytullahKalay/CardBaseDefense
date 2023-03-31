using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;

    private float _horizontalInput;
    private float _verticalInput;
    private Vector2 _inputs;
    
    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _inputs = new Vector2(_horizontalInput, _verticalInput);
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)_inputs * (Time.fixedDeltaTime * speedMultiplier);
    }
}