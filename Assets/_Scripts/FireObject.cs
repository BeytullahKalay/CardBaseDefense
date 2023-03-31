using UnityEngine;

public class FireObject : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float ifNotHitAnyTargetDestroyAfterSeconds = 3f;

    private Vector3 _direction;
    private LayerMask _enemyLayer;
    private int _damage;
    private bool _destroyed;

    public void Initialize(Transform fireTransform, Transform target,int damage)
    {
        _direction = (target.position - fireTransform.position).normalized;
        _enemyLayer = target.gameObject.layer;
        _damage = damage;
        Invoke("AttemptToDestroy",ifNotHitAnyTargetDestroyAfterSeconds);
    }

    private void Update()
    {
        transform.position += _direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == _enemyLayer)
        {
            Debug.Log("in give damage");
            col.GetComponent<HealthSystem>().TakeDamage?.Invoke(_damage);
            Destroy(gameObject);
        }
    }

    private void AttemptToDestroy()
    {
        if (_destroyed) return;

        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        _destroyed = true;
    }
}