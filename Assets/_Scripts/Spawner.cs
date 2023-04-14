using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoSingleton<Spawner>
{
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private Transform spawnPosition;

    [SerializeField] private GameObject spawnObject;

    public List<GameObject> SpawnedEnemies => spawnedEnemies;
    
    public bool WaveCleared { get; private set; }

    private void Awake()
    {
        WaveCleared = true; // true for default
    }

    private void OnEnable()
    {
        EventManager.CallTheWave += CallTheWave;
        EventManager.CheckIsWaveCleared += CheckIsWaveCleared;
    }

    private void OnDisable()
    {
        EventManager.CallTheWave -= CallTheWave;
        EventManager.CheckIsWaveCleared -= CheckIsWaveCleared;
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

        WaveCleared = false;
    }

    private void CheckIsWaveCleared()
    {
        WaveCleared = SpawnedEnemies.Count <= 0;
        EventManager.WaveCompleted?.Invoke(WaveCleared);
    }
}