/*
 * Using this script for resetting navmesh after testing game
 */

using NavMeshPlus.Components;
using UnityEngine;

public class NavmeshReset : MonoBehaviour
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
}
