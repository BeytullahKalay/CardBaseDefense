using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class HurtAnimation : MonoBehaviour
{
    [Header("Animation Values")]
    [SerializeField] private float animationForce;
    [SerializeField] private float animationDuration;
    [SerializeField] private int vibration;
    [SerializeField] private float elasticity;

    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
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
        transform.DOPunchScale(Vector3.one * animationForce, animationDuration, vibration, elasticity);
    }
}
