using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoSingleton<TilemapManager>
{
    [SerializeField] private Tilemap cloudTilemap;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap decorationTilemap;
    [SerializeField] private Tilemap undergroundTilemap;
    [SerializeField] private Tilemap bushTilemap;


    public Tilemap CloudTilemap => cloudTilemap;
    public Tilemap GroundTilemap => groundTilemap;
    public Tilemap DecorationTilemap => decorationTilemap;
    public Tilemap UndergroundTilemap => undergroundTilemap;
    public Tilemap BushTilemap => bushTilemap;




    [SerializeField] private float moveSpeed = 1;
    
    private void FixedUpdate()
    {
        cloudTilemap.transform.Translate(Vector3.right * (moveSpeed * Time.fixedDeltaTime));
    }
}
