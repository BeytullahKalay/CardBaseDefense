using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;

    private float _horizontalInput;
    private float _verticalInput;

    private void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        var inputs = new Vector2(_horizontalInput, _verticalInput);
        transform.position += (Vector3)inputs * (Time.fixedTime * speedMultiplier);
    }
}
