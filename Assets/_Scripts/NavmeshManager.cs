using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NavmeshManager : MonoSingleton<NavmeshManager>
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap undergroundTilemap;
    [SerializeField] private Tilemap decorationTilemap;
        
    private NavMeshSurface _surface;

    private void Awake()
    {
        _surface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        _surface.UpdateNavMesh(_surface.navMeshData);
    }

    public void UpdateSurfaceData()
    {
        _surface.UpdateNavMesh(_surface.navMeshData);
    }


    public Tilemap GroundTilemap => groundTilemap;
    public Tilemap UndergroundTilemap => undergroundTilemap;
    public Tilemap DecorationTilemap => decorationTilemap;
}
