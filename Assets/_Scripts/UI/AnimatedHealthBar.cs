using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthSystem))]
public class AnimatedHealthBar : MonoBehaviour,IHasHealthbarSlider
{
    [SerializeField] private Slider animatedSlider;
    [SerializeField] private float animationSpeed = 5f;

    private HealthSystem _baseHealth;

    private Tween _tween;

    private void Awake()
    {
        _baseHealth = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        _baseHealth.UpdateSlider += AnimateSlider;
    }

    private void OnDisable()
    {
        _baseHealth.UpdateSlider -= AnimateSlider;
    }

    private void Start()
    {
        animatedSlider.value = 1;
        animatedSlider.gameObject.SetActive(false);
    }


    private void AnimateSlider()
    {
        animatedSlider.gameObject.SetActive(true);
        var sliderVal = animatedSlider.value;
        var desVal = (float)_baseHealth.Health / _baseHealth.MaxHealth;
        
        _tween?.Kill();
        _tween = DOTween.To(() => sliderVal, x => sliderVal = x, desVal, animationSpeed)
            .SetSpeedBased().OnUpdate(() => { animatedSlider.value = sliderVal; });
    }

    public GameObject GetHealthBar()
    {
        return animatedSlider.gameObject;
    }
}