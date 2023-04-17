using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BaseHealth))]
public class BaseAnimatons : MonoBehaviour
{

    [SerializeField] private float animationForce;
    [SerializeField] private float animationDuration;
    [SerializeField] private int vibration;
    [SerializeField] private float elastisity;
    
    private BaseHealth _baseHealth;

    private Tween _tween;


    private void Awake()
    {
        _baseHealth = GetComponent<BaseHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _baseHealth.TakeDamage?.Invoke(1);
        }
    }

    private void OnEnable()
    {
        _baseHealth.TakeDamage += PlayHurtAnimation;
    }

    private void OnDisable()
    {
        _baseHealth.TakeDamage -= PlayHurtAnimation;
    }

    private void PlayHurtAnimation(int dmg)
    {
        transform.DOPunchScale(Vector3.one * animationForce, animationDuration, vibration, elastisity);
    }
}
