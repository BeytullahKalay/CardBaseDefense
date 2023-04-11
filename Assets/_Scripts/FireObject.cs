using System;
using System.Collections;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float ifNotHitAnyTargetDestroyAfterSeconds = 3f;

    private Pooler _pooler;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private int _damage;
    [SerializeField] private bool _hit;

    private IEnumerator _coroutine;

    private void Awake()
    {
        _pooler = Pooler.Instance;
    }

    public void Initialize(Transform fireTransform, Transform target, int damage)
    {
        _hit = false;
        transform.position = fireTransform.position;
        _direction = (target.position - fireTransform.position).normalized;
        _targetLayerMask = target.gameObject.layer;
        _damage = damage;
        
        
        _coroutine = IfNotHitTargetDestroyCo(ifNotHitAnyTargetDestroyAfterSeconds);
        StartCoroutine(_coroutine);
    }

    private void FixedUpdate()
    {
        if (!_hit) transform.position += _direction * (speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var colLayer = LayerMask.LayerToName(col.gameObject.layer);
        var targetLayer = LayerMask.LayerToName(_targetLayerMask);

        if (colLayer == targetLayer)
        {
            _hit = true;
            col.GetComponent<HealthSystem>().TakeDamage?.Invoke(_damage);
            StopCoroutine(_coroutine);
            
            var particleCanvas = _pooler.ParticleTextPool.Get();
            particleCanvas.GetComponent<ParticleCanvas>().PlayTextAnimation(_damage.ToString(),
                col.transform.position);
            _pooler.BulletPool.Release(gameObject);
        }
    }

    private IEnumerator IfNotHitTargetDestroyCo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (_hit) yield return null;
        _pooler.BulletPool.Release(gameObject);
    }
}