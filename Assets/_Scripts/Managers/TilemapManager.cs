using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoSingleton<TilemapManager>
{
    [SerializeField] private Tilemap waterTilemap;
    [SerializeField] private Tilemap groundTilemap;

    public Tilemap WaterTilemap => waterTilemap;
    public Tilemap GroundTilemap => groundTilemap;
}
