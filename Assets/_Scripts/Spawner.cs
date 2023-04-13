using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoSingleton<Spawner>
{
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private Transform spawnPosition;

    [SerializeField] private GameObject spawnObject;

    public List<GameObject> SpawnedEnemies => spawnedEnemies;

    public bool WaveCleared => SpawnedEnemies.Count == 0;

    private void OnEnable()
    {
        EventManager.CallTheWave += CallTheWave;
    }

    private void OnDisable()
    {
        EventManager.CallTheWave -= CallTheWave;
    }

    private void CallTheWave()
    {
        var obj = Instantiate(spawnObject, spawnPosition.position, Quaternion.identity);

        foreach (Transform child in obj.transform)
        {
            if (child.TryGetComponent<IOnBoard>(out var enemy))
            {
                spawnedEnemies.Add(enemy.BoardedTransform.gameObject);
            }
        }
    }
}