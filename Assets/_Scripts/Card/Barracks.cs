using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour, IActionCard
{
    [SerializeField] private int maxSoliderAmount = 3;
    
    [SerializeField] private float pathRadius = 3f;
    [SerializeField] private float timeBetweenPaths = 10f;

    [SerializeField] private GameObject soliderPrefab;

    [SerializeField] private List<GameObject> spawnedSolider = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.WaveCompleted += TrySpawnASolider;
    }

    private void OnDisable()
    {
        EventManager.WaveCompleted -= TrySpawnASolider;
    }

    private void Start()
    {
        TrySpawnASolider(true);
    }

    private void TrySpawnASolider(bool isWaveCompleted)
    {
        if(!isWaveCompleted) return;

        if (spawnedSolider.Count < maxSoliderAmount)
        {
            var obj = Instantiate(soliderPrefab);
            obj.GetComponent<DoRandomPath>().Initialize(transform,pathRadius,timeBetweenPaths,transform.position);
            obj.GetComponent<HealthSystem>().OnDead += ClearNullObjectFromList;
            spawnedSolider.Add(obj); 
        }
    }

    private void ClearNullObjectFromList()
    {
        spawnedSolider.RemoveAll(item => item == null);
    }

    public void Enable(bool state)
    {
        this.enabled = state;
    }
}
