using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class HurtAnimation : MonoBehaviour
{
    [Header("Animation Values")] [SerializeField]
    private float animationForce;

    [SerializeField] private float animationDuration;
    [SerializeField] private int vibration;

    private HealthSystem _healthSystem;

    private Tween _tween;

    private Vector3 _startScale;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        _startScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayHurtAnimation(12);
        }
    }

    private void OnEnable()
    {
        _healthSystem.TakeDamage += PlayHurtAnimation;
    }

    private void OnDisable()
    {
        _healthSystem.TakeDamage -= PlayHurtAnimation;
    }

    private void PlayHurtAnimation(int dmg)
    {
        _tween?.Kill();
        _tween = transform.DOShakeScale(animationDuration, Vector3.one * animationForce, vibration).OnComplete(() =>
        {
            transform.DOScale(_startScale, .1f);
        });
    }
}