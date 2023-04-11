using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform checkGroundPoint;

    private Vector2 _basePosition;
    private Vector2 _direction;

    private NavmeshManager _navmeshManager;

    private bool _isGroundTileDetected;

    private void Awake()
    {
        _basePosition = GameManager.Instance.BaseTransform.position;
        _navmeshManager = NavmeshManager.Instance;
    }

    private void Start()
    {
        _direction = (_basePosition - (Vector2)transform.position).normalized;
        transform.right = _direction;
    }

    private void Update()
    {
        var pos = checkGroundPoint.position;
        var tileAnchor = _navmeshManager.GroundTilemap.tileAnchor;
        var mousePosVector3  = new Vector3(pos.x,pos.y,0) - new Vector3(tileAnchor.x, tileAnchor.y, 0);
        _isGroundTileDetected = _navmeshManager.GroundTilemap.HasTile(Vector3Int.RoundToInt(mousePosVector3));
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_isGroundTileDetected) return;
        transform.position += transform.right * (speed * Time.fixedDeltaTime);
    }
}