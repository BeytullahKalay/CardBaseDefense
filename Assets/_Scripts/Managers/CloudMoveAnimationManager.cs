using UnityEngine;
using UnityEngine.Tilemaps;

public class CloudMoveAnimationManager : MonoBehaviour
{
    [Header("Cloud Destroy Values")]
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float xOffset; // it should be radius of cloud spawner
    [SerializeField] private float timeBetweenChecks = 5f;
    [SerializeField] private Transform rightSpawnerTransform;

    private float _lastDestroyTime = float.MinValue;

    private Tilemap _cloudTilemap;
    private Tilemap _cloudShadow;

    private void Awake()
    {
        _cloudTilemap = TilemapManager.Instance.CloudTilemap;
        _cloudShadow = TilemapManager.Instance.CloudShadow;
    }

    private void Update()
    {
        CheckDestroyCloudTile();
    }
    
    private void CheckDestroyCloudTile()
    {
        if (!(Time.time > _lastDestroyTime)) return;
        
        
        foreach (var pos in _cloudTilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if (!_cloudTilemap.HasTile(localPlace)) continue;
            
            
            if ((localPlace + _cloudTilemap.transform.position).x > rightSpawnerTransform.position.x + xOffset)
            {
                _cloudTilemap.SetTile(localPlace, null);
                _cloudShadow.SetTile(localPlace, null);
            }
        }
        _lastDestroyTime = Time.time + timeBetweenChecks;
    }

    private void FixedUpdate()
    {
        _cloudTilemap.transform.Translate(Vector3.right * (moveSpeed * Time.fixedDeltaTime));
        _cloudShadow.transform.Translate(Vector3.right * (moveSpeed * Time.fixedDeltaTime));
    }
}
