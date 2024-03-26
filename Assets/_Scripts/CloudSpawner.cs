using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private CloudSpawnerData _cloudSpawnerData;

    private Tilemap _cloudTilemap;
    private Tilemap _cloudShadow;

    private void Awake()
    {
        _cloudTilemap = TilemapManager.Instance.CloudTilemap;
        _cloudShadow = TilemapManager.Instance.CloudShadow;
    }

    private void Start()
    {
        for (int i = 0; i < _cloudSpawnerData.DrawAmount; i++)
        {
            var pos = (Random.insideUnitCircle * _cloudSpawnerData.SpawnRadius) + (Vector2)transform.position;
            var randomTile = GetRandomTile();
            _cloudTilemap.SetTile(_cloudTilemap.WorldToCell(pos), randomTile);
            _cloudShadow.SetTile(_cloudShadow.WorldToCell(pos + new Vector2(0, _cloudShadow.transform.position.y)),
                randomTile);
        }
    }

    private void Update()
    {
        int numOfTile = 0;
        var maxDistance = Mathf.PI * _cloudSpawnerData.SpawnRadius / 3;
        numOfTile = GetTileAmountInSphere(maxDistance, numOfTile);
        DrawTile(numOfTile);
    }

    private void DrawTile(int numOfTile)
    {
        if (numOfTile >= _cloudSpawnerData.DrawAmount) return;


        for (int i = 0; i < _cloudSpawnerData.DrawAmount - numOfTile; i++)
        {
            var rand = (Random.insideUnitCircle * _cloudSpawnerData.SpawnRadius);

            var cloudPos = rand + (Vector2)transform.position - (Vector2)_cloudTilemap.transform.position;
            var randomTile = GetRandomTile();
            _cloudTilemap.SetTile(Vector3Int.RoundToInt(cloudPos), randomTile);
            _cloudShadow.SetTile(Vector3Int.RoundToInt(cloudPos), randomTile);
        }
    }

    private int GetTileAmountInSphere(float maxDistance, int numOfTile)
    {
        for (var x = -(int)maxDistance; x < maxDistance; x++)
        {
            for (var y = -(int)maxDistance; y < maxDistance; y++)
            {
                var tile = _cloudTilemap.GetTile(
                    _cloudTilemap.WorldToCell(new Vector3Int(x, y, 0) + transform.position));
                if (tile)
                {
                    numOfTile++;
                }
            }
        }

        return numOfTile;
    }

    private Tile GetRandomTile()
    {
        return _cloudSpawnerData.TileList[Random.Range(0, _cloudSpawnerData.TileList.Count)];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _cloudSpawnerData.SpawnRadius);
    }
}