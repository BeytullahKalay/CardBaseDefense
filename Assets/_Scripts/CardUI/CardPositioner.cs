using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CardPositioner : MonoBehaviour
{
    [SerializeField] private float radius;
    private float fanAngle;
    [SerializeField] private float upperAmount;
    [SerializeField] private float duration = 2f;

    private List<Transform> _cardList = new List<Transform>();

    private CanvasGroup _cardsCanvasGroup;

    public CanvasGroup CanvasGroup => _cardsCanvasGroup;

    public List<Transform> CardList => _cardList;
    
    private void Awake()
    {
        _cardsCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventManager.SetCardsPosition += SetCardsPosition;
    }

    private void OnDisable()
    {
        EventManager.SetCardsPosition -= SetCardsPosition;
    }

    private void SetCardsPosition()
    { float cardAngleStep = 0;
        float startAngle = 0;
        
        fanAngle = _cardList.Count * 2;

        if (_cardList.Count > 1)
        {
            cardAngleStep =fanAngle / (_cardList.Count - 1);
            startAngle = -(fanAngle / 2f);
        }
        else
        {
            cardAngleStep = 0;
            startAngle = 0;
        }

        float sum = 0;

        for (int i = 0; i < _cardList.Count; i++)
        {
            var cardAngle = startAngle + i * cardAngleStep;
            var x = Mathf.Sin(cardAngle * Mathf.Deg2Rad) * radius;
            var y = Mathf.Cos(cardAngle * Mathf.Deg2Rad) * radius;
            sum += y;

            _cardList[i].GetComponent<RectTransform>().DOLocalMove(new Vector2(x, y), duration);
            _cardList[i].GetComponent<RectTransform>().DORotate(new Vector3(0f, 0f, -cardAngle), duration);

            _cardList[i].GetComponent<ClassicCard>().AssignCardPositionAndRotation(new Vector3(0f, 0f, -cardAngle));
        }

        var average = sum / _cardList.Count;
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -average + upperAmount, 0);
    }
}