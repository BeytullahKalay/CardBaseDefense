using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(IUnit))]
public class DoRandomPath : MonoBehaviour
{
    private Vector2 _pathCenterPosition;
    
    private float _moveRadius = 3f;
    private float _timeBetweenPaths = 10f;
    private float _nextPathTime = float.MinValue;

    private IUnit _unit;
    

    private void Awake()
    {
        _unit = GetComponent<IUnit>();
    }

    public void Initialize(Transform center, float moveRadius, float timeBetweenPaths,Vector3 spawnPosition)
    {
        _pathCenterPosition = center.position;
        _moveRadius = moveRadius;
        _timeBetweenPaths = timeBetweenPaths;
        transform.position = spawnPosition;
        SetPathPosition();
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        if (Time.time > _nextPathTime && _unit.UnitStates != UnitStates.Attack)
        {
            SetPathPosition();
        }
    }

    private void SetPathPosition()
    {
        _unit.MovePos = (Vector2)_pathCenterPosition + Random.insideUnitCircle * _moveRadius;
        _nextPathTime = Time.time + _timeBetweenPaths;
    }
}