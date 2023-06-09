using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class WaveCallerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject canvasTransform;
    [SerializeField] private float duration = .1f;
    [SerializeField] private Ease ease;
    [SerializeField] private Transform buttonAndText;
    
    private Vector3 _scale;

    private Tween _tween;

    private GameManager _gm;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _gm = GameManager.Instance;
    }

    private void OnEnable()
    {
        EventManager.SetMouseStateTo += OpenCloseCanvasByMouseState;
    }

    private void OnDisable()
    {
        EventManager.SetMouseStateTo -= OpenCloseCanvasByMouseState;
    }

    private void Start()
    {
        _scale = buttonAndText.localScale;
        buttonAndText.localScale = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Spawner.Instance.WaveCleared || MouseStateManager.Instance.MouseState != MouseState.Available)
        {
            _gm.UpdateAllCardsState();
            return;
        }
        OpenUI();
    }

    private void OpenUI()
    {
        _tween?.Kill();
        _tween = buttonAndText.DOScale(_scale, duration).SetEase(ease);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseUI();
    }

    private void CloseUI()
    {
        _tween?.Kill();
        _tween = buttonAndText.DOScale(Vector3.zero, duration).SetEase(ease);
    }

    public void CallWaveButton()
    {
        EventManager.CallTheWave?.Invoke();
        _audioSource.Play();
        CloseUI();
    }

    private void OpenCloseCanvasByMouseState(MouseState mouseState)
    {
        canvasTransform.SetActive(mouseState == MouseState.Available);
    }

}