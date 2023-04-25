using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPath : MonoBehaviour
{
    [SerializeField] private PunchData punchData;
    [SerializeField] private Transform movePosTransform;
    [SerializeField] private float moveRadius = 3f;
    [SerializeField] private float timeBetweenPaths = 10f;

    private float nextPathTime = float.MinValue;

    private void Awake()
    {
        punchData.MovePos = transform.position;
        SetPathPosition();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     print("changed");
        //     SetPathPosition();
        // }

        if (Time.time > nextPathTime)
        {
            SetPathPosition();
        }
    }

    private void SetPathPosition()
    {
        punchData.MovePos = (Vector2)movePosTransform.position + Random.insideUnitCircle * moveRadius;
        nextPathTime = Time.time + timeBetweenPaths;
    }
}
