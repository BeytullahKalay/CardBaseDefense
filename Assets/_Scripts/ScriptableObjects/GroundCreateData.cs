using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "ScriptableObjects/GroundCreateData")]
public class GroundCreateData : ScriptableObject
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap underGroundTilemap;
    [SerializeField] private TileBase tile;
    [SerializeField] private int sizeOfGround = 3;
    [SerializeField] private NavMeshSurface surface;

    public Tilemap GroundTilemap => groundTilemap;
    public Tilemap UnderGroundTilemap => underGroundTilemap;
    public TileBase TileBase => tile;
    public int SizeOfGround => sizeOfGround;
    public NavMeshSurface Surface => surface;
}
