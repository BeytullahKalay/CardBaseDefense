using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    public GameObject GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }
}
