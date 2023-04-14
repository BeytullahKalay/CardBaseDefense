using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaveCallerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float duration = .1f;
    [SerializeField] private Ease ease;
    [SerializeField] private Transform buttonAndText;
    
    
    private Vector3 _scale;

    private Tween _tween;

    private GameManager _gm;

    private void Awake()
    {
        _gm = GameManager.Instance;
    }

    private void Start()
    {
        _scale = buttonAndText.localScale;
        buttonAndText.localScale = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Spawner.Instance.WaveCleared)
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
        CloseUI();
    }
}