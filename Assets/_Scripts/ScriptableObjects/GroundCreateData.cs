using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "ScriptableObjects/GroundCreateData")]
public class GroundCreateData : ScriptableObject
{
    [SerializeField] private TileBase tile;
    [SerializeField] private TileBase decorationTile;
    [SerializeField] private TileBase bushTile;
    [SerializeField] private int sizeOfGround = 3;
    [SerializeField] private Color placeableColor;
    [SerializeField] private Color notPlaceableColor;

    public TileBase TileBase => tile;
    public TileBase DecorationTile => decorationTile;
    public TileBase BushTile => bushTile;
    public int SizeOfGround => sizeOfGround;

    public Color PlaceableColor => placeableColor;
    public Color NotPlaceableColor => notPlaceableColor;
}
