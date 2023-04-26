using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]private CloudSpawnerData _cloudSpawnerData;
    
    private Tilemap _cloudTilemap;

    private void Awake()
    {
        _cloudTilemap = TilemapManager.Instance.CloudTilemap;
    }

    private void Start()
    {
        for (int i = 0; i < _cloudSpawnerData.DrawAmount; i++)
        {
            var pos = (Random.insideUnitCircle * _cloudSpawnerData.SpawnRadius) + (Vector2)transform.position;
            _cloudTilemap.SetTile(_cloudTilemap.WorldToCell(pos), GetRandomTile());
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
        if (numOfTile <_cloudSpawnerData.DrawAmount)
        {
            for (int i = 0; i < _cloudSpawnerData.DrawAmount - numOfTile; i++)
            {
                var pos = (Random.insideUnitCircle * _cloudSpawnerData.SpawnRadius) + (Vector2)transform.position -
                          (Vector2)_cloudTilemap.transform.position;

                _cloudTilemap.SetTile(Vector3Int.RoundToInt(pos), GetRandomTile());
            }
        }
    }

    private int GetTileAmountInSphere(float maxDistance, int numOfTile)
    {
        for (var x = -(int)maxDistance; x < maxDistance; x++)
        {
            for (var y = -(int)maxDistance; y < maxDistance; y++)
            {
                var a = _cloudTilemap.GetTile(
                    _cloudTilemap.WorldToCell(new Vector3Int(x, y, 0) + transform.position));
                if (a)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _cloudSpawnerData.SpawnRadius);
    }
}
