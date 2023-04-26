using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoSingleton<TilemapManager>
{
    [SerializeField] private Tilemap cloudTilemap;
    [SerializeField] private Tilemap groundTilemap;

    public Tilemap CloudTilemap => cloudTilemap;
    public Tilemap GroundTilemap => groundTilemap;

    [SerializeField] private float moveSpeed = 1;
    
    private void FixedUpdate()
    {
        cloudTilemap.transform.Translate(Vector3.right * (moveSpeed * Time.fixedDeltaTime));
    }
}
