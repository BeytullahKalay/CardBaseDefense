using System;
using Unity.Mathematics;
using UnityEngine;

public class SpawnRandomEnemyOnBoat : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform1;
    [SerializeField] private Transform spawnTransform2;

    private LandPassengers _landPassengers;

    private void Awake()
    {
        _landPassengers = GetComponent<LandPassengers>();
    }

    private void Start()
    {
        var obj1 = Instantiate(EnemyManager.Instance.GetRandomEnemy(), spawnTransform1.position, quaternion.identity);
        var obj2 = Instantiate(EnemyManager.Instance.GetRandomEnemy(), spawnTransform2.position, quaternion.identity);
        
        SetParentOfObjectToTransform(obj1);
        SetParentOfObjectToTransform(obj2);
        
        AddObjectToSpawnedEnemiesList(obj1);
        AddObjectToSpawnedEnemiesList(obj2);

        AddObjectToLandPassengersList(obj1);
        AddObjectToLandPassengersList(obj2);
    }

    private void SetParentOfObjectToTransform(GameObject comingGameObj)
    {
        comingGameObj.transform.SetParent(transform);
    }

    private void AddObjectToSpawnedEnemiesList(GameObject comingGameObj)
    {
        if (comingGameObj.TryGetComponent<IOnBoard>( out var enemy))
        {
            Spawner.Instance.SpawnedEnemies.Add(enemy.BoardedTransform.gameObject);
        }
        else
        {
            Debug.LogError("No IOnBoard script on " + comingGameObj.name + " !!");
        }
    }

    private void AddObjectToLandPassengersList(GameObject comingGameObj)
    {
        if (comingGameObj.TryGetComponent<IOnBoard>( out var enemy))
        {
            _landPassengers.AddPassengerToList(enemy);
        }
        else
        {
            Debug.LogError("No IOnBoard script on " + comingGameObj.name + " !!");
        }
    }
}