using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class WaveCallerUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image fillImage;
    [SerializeField] private GameObject generalGameObject;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform knightImageTransform;
    [SerializeField] private float fillTime = 3;
    [SerializeField] private float openAndCloseFadingDuration = .1f;
    [SerializeField] private float dischargeTime = 1;
    
    private bool _isOpen;
    private bool _lock;
    
    private Tween _canvasAlphaTween;
    private Tween _imageFillAmountTween;
    
    private IEnumerator _coroutine;


    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnEnable()
    {
        EventManager.CallTheWave += LockUI;
        EventManager.WaveCompleted += UnlockUI;
        EventManager.WaveCompleted += ResetCallingWaveUI;
    }

    private void OnDisable()
    {
        EventManager.CallTheWave -= LockUI;
        EventManager.WaveCompleted -= UnlockUI;
        EventManager.WaveCompleted -= ResetCallingWaveUI;

    }
    
    private void Start()
    {
        canvasGroup.alpha = 0;
        generalGameObject.SetActive(false);
        fillImage.fillAmount = 0;
        _coroutine = CloseUIWithFading_CO(1f);
    }

    private void LockUI()
    {
        _lock = true;
    }

    private void UnlockUI(bool state)
    {
        _lock = !state;
    }
    
    private void ResetCallingWaveUI(bool state)
    {
        if (!state) return;
        fillImage.fillAmount = 0;
    }

    private void CloseWithFadingCallerUI()
    {
        _canvasAlphaTween?.Kill();
        var canvasAlpha = canvasGroup.alpha;
        _canvasAlphaTween = DOTween
            .To(() => canvasAlpha, x => canvasAlpha = x, 0, openAndCloseFadingDuration).SetSpeedBased()
            .OnUpdate(() => { canvasGroup.alpha = canvasAlpha; }).OnComplete(() =>
            {
                generalGameObject.SetActive(false);
            });
    }
    
    private IEnumerator CloseUIWithFading_CO(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        CloseWithFadingCallerUI();
    }

    private void Update()
    {
        if (_lock) return;


        if (Input.GetKey(KeyCode.Space))
        {
            if (_isOpen) return;
            _isOpen = true;
            generalGameObject.SetActive(true);

            // open canvas
            _canvasAlphaTween?.Kill();
            var canvasAlpha = canvasGroup.alpha;
            _canvasAlphaTween = DOTween.To(() => canvasAlpha, x => canvasAlpha = x, 1, openAndCloseFadingDuration)
                .OnUpdate(() => { canvasGroup.alpha = canvasAlpha; }).SetSpeedBased();

            // fill the fill image
            _imageFillAmountTween?.Kill();
            var imageFillAmount = fillImage.fillAmount;
            _imageFillAmountTween = DOTween.To(() => imageFillAmount, x => imageFillAmount = x, 1, fillTime)
                .SetSpeedBased().OnUpdate(() => { fillImage.fillAmount = imageFillAmount; }).OnComplete(() =>
                {
                    // completed calling stuff
                    knightImageTransform.DOShakePosition(.35f, 10, 30);
                    animator.SetTrigger("Attack");
                    StartCoroutine(_coroutine);
                    _audioSource.Play();
                    EventManager.CallTheWave?.Invoke();
                });
        }
        else
        {
            _isOpen = false;

            // discharge fill image
            _imageFillAmountTween?.Kill();
            var imageFillAmount = fillImage.fillAmount;
            _imageFillAmountTween = DOTween.To(() => imageFillAmount, x => imageFillAmount = x, 0, dischargeTime)
                .SetSpeedBased().OnUpdate(() => { fillImage.fillAmount = imageFillAmount; }).OnComplete(() =>
                {
                    // close canvas
                    _canvasAlphaTween?.Kill();
                    var canvasAlpha = canvasGroup.alpha;
                    _canvasAlphaTween = DOTween
                        .To(() => canvasAlpha, x => canvasAlpha = x, 0, openAndCloseFadingDuration).SetSpeedBased()
                        .OnUpdate(() => { canvasGroup.alpha = canvasAlpha; }).OnComplete(() =>
                        {
                            generalGameObject.SetActive(false);
                        });
                });
        }
    }
}