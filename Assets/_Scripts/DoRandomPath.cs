using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PuncherStateManager))]
public class DoRandomPath : MonoBehaviour
{
    private Vector2 _pathCenterPosition;
    
    private float _moveRadius = 3f;
    private float _timeBetweenPaths = 10f;
    private float _nextPathTime = float.MinValue;

    private PuncherStateManager _puncherStateManager;
    

    private void Awake()
    {
        _puncherStateManager = GetComponent<PuncherStateManager>();
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
        if (Time.time > _nextPathTime && _puncherStateManager.UnitStates != UnitStates.Attack)
        {
            SetPathPosition();
        }
    }

    private void SetPathPosition()
    {
        _puncherStateManager.MovePos = (Vector2)_pathCenterPosition + Random.insideUnitCircle * _moveRadius;
        _nextPathTime = Time.time + _timeBetweenPaths;
    }
}