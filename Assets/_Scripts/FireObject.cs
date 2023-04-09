using UnityEditor.Rendering;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float ifNotHitAnyTargetDestroyAfterSeconds = 3f;

    private Pooler _pooler;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private int _damage;
    [SerializeField] private bool _hit;
    [SerializeField] private GameObject _particleCanvas;

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
        
        
        
        //Invoke("AttemptToDestroy", ifNotHitAnyTargetDestroyAfterSeconds);
    }

    private void FixedUpdate()
    {
        if (!_hit)
            transform.position += _direction * (speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var colLayer = LayerMask.LayerToName(col.gameObject.layer);
        var targetLayer = LayerMask.LayerToName(_targetLayerMask);
        
        
        
        if (colLayer == targetLayer)
        {
            _hit = true;
            col.GetComponent<HealthSystem>().TakeDamage?.Invoke(_damage);
            
            
            _particleCanvas = _pooler.ParticleTextPool.Get();
            _particleCanvas.GetComponent<ParticleCanvas>().PlayTextAnimation(_damage.ToString(),
                col.transform.position, fadeDuration);
            _pooler.BulletPool.Release(gameObject);
        }
    }



    //TODO: bu calismiyor
    // private void AttemptToDestroy()
    // {
    //     if (_hit) return;
    //     
    //     print("released");
    //     _pooler.BulletPool.Release(gameObject);
    //     ReleaseTheCanvasParticle();
    // }
}