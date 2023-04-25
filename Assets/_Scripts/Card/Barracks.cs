using System.Collections.Generic;
using UnityEngine;

public class Barracks : ActionCard
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TrySpawnASolider(true);
        }
    }

    private void TrySpawnASolider(bool isWaveCompleted)
    {
        print("1");
        
        
        if(!isWaveCompleted) return;
        print("2");

        if (spawnedSolider.Count < maxSoliderAmount)
        {
            print("3");
            var obj = Instantiate(soliderPrefab);
            obj.GetComponent<DoRandomPath>().Initialize(transform,pathRadius,timeBetweenPaths,transform.position);
            obj.GetComponent<HealthSystem>().OnDead += RemoveObject;
            spawnedSolider.Add(obj); 
        }
    }

    private void RemoveObject()
    {
        
    }
}
