using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoSingleton<Spawner>
{
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private Transform spawnPosition;

    [SerializeField] private GameObject spawnObject;

    [SerializeField] private float startDistanceOnSpawn = 30;

    public List<GameObject> SpawnedEnemies => spawnedEnemies;

    public bool WaveCleared { get; private set; }

    private Vector2 _mapSize;

    private void Awake()
    {
        WaveCleared = true; // true for default
    }

    private void Start()
    {
        ChangeSpawnPositionRandomly();
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
        ChangeSpawnPositionRandomly();

        Instantiate(spawnObject, spawnPosition.position, Quaternion.identity);

        WaveCleared = false;
    }

    private void ChangeSpawnPositionRandomly()
    {
        var newPos = Random.insideUnitCircle * Vector2.one * startDistanceOnSpawn + Vector2.one * GetMapSize();
        spawnPosition.position = newPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            print(GetMapSize());
        }
    }

    private void CheckIsWaveCleared()
    {
        WaveCleared = SpawnedEnemies.Count <= 0;
        EventManager.WaveCompleted?.Invoke(WaveCleared);
    }

    public void UpdateMaxMovePosition(Vector3 start, Vector3 end)
    {
        _mapSize.x = Mathf.Max(_mapSize.x, Mathf.Max(Mathf.Abs(start.x), Mathf.Abs(end.x)));
        _mapSize.y = Mathf.Max(_mapSize.y, Mathf.Max(Mathf.Abs(start.y), Mathf.Abs(end.y)));
    }

    private float GetMapSize()
    {
        return Mathf.Max(_mapSize.x, _mapSize.y);
    }
}