using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObjects/Cloud Spawner Data",fileName = "Cloud Spawner")]
public class CloudSpawnerData : ScriptableObject
{
    [SerializeField] private float spawnRadius;
    [SerializeField] private List<Tile> tileList = new List<Tile>();
    [SerializeField] private int drawAmount = 5;

    public float SpawnRadius => spawnRadius;

    public int DrawAmount => drawAmount;

    public List<Tile> TileList => tileList;
}
