using NavMeshPlus.Components;

public class NavmeshManager : MonoSingleton<NavmeshManager>
{
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
}
