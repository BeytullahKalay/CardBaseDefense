using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "ScriptableObjects/GroundCreateData")]
public class GroundCreateData : ScriptableObject
{
    [SerializeField] private TileBase tile;
    [SerializeField] private TileBase decorationTile;
    [SerializeField] private List<TileBase> bushTile = new List<TileBase>();
    [SerializeField] private int sizeOfGround = 3;
    [SerializeField] private Color placeableColor;
    [SerializeField] private Color notPlaceableColor;
    [SerializeField] [Range(0, 1)] private float bushSpawnChance = .05f;

    public TileBase TileBase => tile;
    public TileBase DecorationTile => decorationTile;
    public TileBase BushTile => Random.value > bushSpawnChance ? null : bushTile[Random.Range(0,bushTile.Count)];
    
    public int SizeOfGround => sizeOfGround;

    public Color PlaceableColor => placeableColor;
    public Color NotPlaceableColor => notPlaceableColor;
}