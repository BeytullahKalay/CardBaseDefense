using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoSingleton<TilemapManager>
{
    [Header("Tilemaps")] [SerializeField] private Tilemap cloudTilemap;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap decorationTilemap;
    [SerializeField] private Tilemap undergroundTilemap;
    [SerializeField] private Tilemap bushTilemap;


    public Tilemap CloudTilemap => cloudTilemap;
    public Tilemap GroundTilemap => groundTilemap;
    public Tilemap DecorationTilemap => decorationTilemap;
    public Tilemap UndergroundTilemap => undergroundTilemap;
    public Tilemap BushTilemap => bushTilemap;

    [Header("Cloud Destroy Values")]
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float xOffset; // it should be radius of cloud spawner
    [SerializeField] private float timeBetweenChecks = 5f;
    [SerializeField] private Transform rightSpawnerTransform;

    private float _lastDestroyTime = float.MinValue;

    private void Update()
    {
        CheckDestroyCloudTile();
    }

    private void CheckDestroyCloudTile()
    {
        if (Time.time > _lastDestroyTime)
        {
            foreach (var pos in cloudTilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

                if (cloudTilemap.HasTile(localPlace))
                {
                    if ((localPlace + cloudTilemap.transform.position).x > rightSpawnerTransform.position.x + xOffset)
                    {
                        cloudTilemap.SetTile(localPlace, null);
                    }
                }
            }
            _lastDestroyTime = Time.time + timeBetweenChecks;
        }
    }

    private void FixedUpdate()
    {
        cloudTilemap.transform.Translate(Vector3.right * (moveSpeed * Time.fixedDeltaTime));
    }
}