using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoSingleton<TilemapManager>
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap cloudTilemap;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap decorationTilemap;
    [SerializeField] private Tilemap undergroundTilemap;
    [SerializeField] private Tilemap bushTilemap;
    [SerializeField] private Tilemap cloudShadow;


    public Tilemap CloudTilemap => cloudTilemap;
    public Tilemap GroundTilemap => groundTilemap;
    public Tilemap DecorationTilemap => decorationTilemap;
    public Tilemap UndergroundTilemap => undergroundTilemap;
    public Tilemap BushTilemap => bushTilemap;
    public Tilemap CloudShadow => cloudShadow;
}