using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoSingleton<TilemapManager>
{
    [SerializeField] private Tilemap waterTilemap;

    public Tilemap WaterTilemap => waterTilemap;
}
