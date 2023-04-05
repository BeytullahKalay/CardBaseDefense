using DG.Tweening;
using TMPro;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float ifNotHitAnyTargetDestroyAfterSeconds = 3f;
    [SerializeField] private GameObject bulletSprite;

    private Pooler _pooler;

    private Vector3 _direction;
    private LayerMask _enemyLayer;
    private int _damage;
    private bool _hit;

    private void Awake()
    {
        _pooler = Pooler.Instance;
    }

    public void Initialize(Transform fireTransform, Transform target, int damage)
    {
        transform.position = fireTransform.position;
        _direction = (target.position - fireTransform.position).normalized;
        _enemyLayer = target.gameObject.layer;
        _damage = damage;
        Invoke("AttemptToDestroy", ifNotHitAnyTargetDestroyAfterSeconds);

        tmpText.text = damage.ToString();
        tmpText.alpha = 1;
        tmpText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!_hit)
            transform.position += _direction * (speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == _enemyLayer)
        {
            _hit = true;
            col.GetComponent<HealthSystem>().TakeDamage?.Invoke(_damage);
            bulletSprite.SetActive(false);

            tmpText.transform.position = col.transform.position + Vector3.up;
            tmpText.gameObject.SetActive(true);
            tmpText.transform.DOMoveY(tmpText.transform.position.y + 2, fadeDuration)
                .OnComplete(() => Destroy(gameObject));
            tmpText.DOFade(0, fadeDuration);
        }
    }

    private void AttemptToDestroy()
    {
        if (_hit) return;
        _pooler.BulletPool.Release(gameObject);
    }
}