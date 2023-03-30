using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BottomUIAnimation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private float duration;
    [SerializeField] private float moveDownAmount = 100;
    
    private Tween _activeTween;
    private RectTransform _rectTransform;
    private Vector3 _pos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _pos = _rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MoveUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MoveDown();
    }

    private void MoveUp()
    {
        _activeTween?.Kill();
        _rectTransform.DOMoveY(_pos.y, duration).SetSpeedBased().SetUpdate(UpdateType.Fixed,false);
    }
    
    private void MoveDown()
    {
        _activeTween?.Kill();
        _rectTransform.DOMoveY(_pos.y - moveDownAmount, duration).SetSpeedBased().SetUpdate(UpdateType.Fixed,false);
    }
}
