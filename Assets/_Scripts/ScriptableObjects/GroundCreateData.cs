using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "ScriptableObjects/GroundCreateData")]
public class GroundCreateData : ScriptableObject
{
    [SerializeField] private TileBase tile;
    [SerializeField] private int sizeOfGround = 3;

    public TileBase TileBase => tile;
    public int SizeOfGround => sizeOfGround;
}
