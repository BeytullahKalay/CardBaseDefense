using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class Test : MonoBehaviour
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
    private bool _waveCalled;

    private Tween _canvasAlphaTween;
    private Tween _imageFillAmountTween;

    private void Start()
    {
        canvasGroup.alpha = 0;
        generalGameObject.SetActive(false);
        fillImage.fillAmount = 0;
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_isOpen) return;

            _isOpen = true;


            generalGameObject.SetActive(true);


            _canvasAlphaTween?.Kill();
            var canvasAlpha = canvasGroup.alpha;
            _canvasAlphaTween = DOTween.To(() => canvasAlpha, x => canvasAlpha = x, 1, openAndCloseFadingDuration)
                .OnUpdate(() => { canvasGroup.alpha = canvasAlpha; }).SetSpeedBased();

            _imageFillAmountTween?.Kill();
            var imageFillAmount = fillImage.fillAmount;
            _imageFillAmountTween = DOTween.To(() => imageFillAmount, x => imageFillAmount = x, 1, fillTime)
                .SetSpeedBased().OnUpdate(() => { fillImage.fillAmount = imageFillAmount; }).OnComplete(() =>
                {
                    knightImageTransform.DOShakePosition(.35f, 10,30);
                    animator.SetTrigger("Attack");
                });

            

        }
        else
        {
            _isOpen = false;

            _imageFillAmountTween?.Kill();
            var imageFillAmount = fillImage.fillAmount;
            _imageFillAmountTween = DOTween.To(() => imageFillAmount, x => imageFillAmount = x, 0, dischargeTime)
                .SetSpeedBased().OnUpdate(() => { fillImage.fillAmount = imageFillAmount; }).OnComplete(() =>
                {
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


        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     EventManager.GameOver?.Invoke();
        // }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     print("here");
        //     EventManager.GameOver?.Invoke();
        // }

        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     CreateCard();
        // }

        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         SetCardsPosition();
        //     }

        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     SoundFXManager.Instance.PlayRandomSoundFXClip(damageSoundClips,transform);
        // }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     print(2f.Remap(0,4,0,100));
        // }

        // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        // if (Physics.Raycast(ray,out hit))
        // {
        //     print(hit.transform.name);
        // }

        // RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //
        // if (hit.collider != null)
        // {
        //     print(hit.collider.gameObject.name);
        // }
    }
}