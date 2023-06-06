using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BottomUIAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float duration;
    [SerializeField] private float moveDownAmount = 100;

    private Tween _activeTween;
    private RectTransform _rectTransform;
    private Vector3 _pos;

    private Spawner _spawner;
    private CardSelectManager _cardSelectManager;

    private bool _isBottomPanelOpen;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        _spawner = Spawner.Instance;
        _cardSelectManager = CardSelectManager.Instance;
    }

    private void OnEnable()
    {
        EventManager.CallTheWave += OnWaveComing;
    }

    private void OnDisable()
    {
        EventManager.CallTheWave -= OnWaveComing;
    }

    private void Start()
    {
        _pos = _rectTransform.anchoredPosition;
        MoveDown();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_spawner.WaveCleared) return;
        MoveUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_cardSelectManager.SelectedCards.Count > 0 && _cardSelectManager.SelectedCards.Count > 0) return;
        MoveDown();
    }

    private void MoveUp()
    {
        _isBottomPanelOpen = true;
        _activeTween?.Kill();
        _rectTransform.DOMoveY(_pos.y, duration).SetSpeedBased().SetUpdate(UpdateType.Fixed, false);
    }

    private void MoveDown()
    {
        _isBottomPanelOpen = false;
        _activeTween?.Kill();
        _rectTransform.DOMoveY(_pos.y - moveDownAmount, duration).SetSpeedBased().SetUpdate(UpdateType.Fixed, false);
    }

    private void OnWaveComing()
    {
        if (_isBottomPanelOpen)
        {
            MoveDown();
        }
    }
}