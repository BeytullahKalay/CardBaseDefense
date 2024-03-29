using UnityEngine;

public class Boat : MonoBehaviour
{
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform checkGroundPoint;

    private Vector2 _basePosition;
    private Vector2 _direction;

    private TilemapManager _tilemapManager;

    private bool _isGroundTileDetected;
    private bool _allUnitsDead;

    private Vector3Int _detectedGroundPosition;

    private LandPassengers _landPassengers;


    private void Awake()
    {
        _landPassengers = GetComponent<LandPassengers>();
        _basePosition = GameManager.Instance.BaseTransform.position;
        _tilemapManager = TilemapManager.Instance;
    }

    private void Start()
    {
        _direction = (_basePosition - (Vector2)transform.position).normalized;
        transform.right = _direction;
    }

    private void Update()
    {
        CheckIsReachedToGround();
    }

    private void CheckIsReachedToGround()
    {
        var pos = checkGroundPoint.position;
        var tileAnchor = _tilemapManager.GroundTilemap.tileAnchor;
        var mapPosition2D = new Vector3(pos.x, pos.y, 0) - new Vector3(tileAnchor.x, tileAnchor.y, 0);
        _detectedGroundPosition = Vector3Int.RoundToInt(mapPosition2D);
        _isGroundTileDetected = _tilemapManager.GroundTilemap.HasTile(_detectedGroundPosition);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_allUnitsDead) return;
        
        
        if (_isGroundTileDetected)
        {
            _landPassengers.LandPassenger(_detectedGroundPosition);
            Destroy(this);
            return;
        }
        
        transform.position += transform.right * (speed * Time.fixedDeltaTime);
    }
}