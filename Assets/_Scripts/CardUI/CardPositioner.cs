using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CardPositioner : MonoBehaviour
{
    [SerializeField] private float radius;
    private float fanAngle;
    [SerializeField] private float upperAmount;
    [SerializeField] private float duration = 2f;
    
    private CanvasGroup _cardsCanvasGroup;

    public CanvasGroup CanvasGroup => _cardsCanvasGroup;

    private void Awake()
    {
        _cardsCanvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        SetCardsPosition();
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
    {
        var allChildren = transform.GetAllChildren();

        float cardAngleStep = 0;
        float startAngle = 0;
        
        fanAngle = allChildren.Count * 2;

        if (allChildren.Count > 1)
        {
            cardAngleStep =fanAngle / (allChildren.Count - 1);
            startAngle = -(fanAngle / 2f);
        }
        else
        {
            cardAngleStep = 0;
            startAngle = 0;
        }

        float sum = 0;

        for (int i = 0; i < allChildren.Count; i++)
        {
            var cardAngle = startAngle + i * cardAngleStep;
            var x = Mathf.Sin(cardAngle * Mathf.Deg2Rad) * radius;
            var y = Mathf.Cos(cardAngle * Mathf.Deg2Rad) * radius;
            sum += y;

            allChildren[i].GetComponent<RectTransform>().DOLocalMove(new Vector2(x, y), duration);
            allChildren[i].GetComponent<RectTransform>().DORotate(new Vector3(0f, 0f, -cardAngle), duration);

            allChildren[i].GetComponent<ClassicCard>().AssignCardPositionAndRotation(new Vector3(0f, 0f, -cardAngle));
        }

        var average = sum / allChildren.Count;
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -average + upperAmount, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetCardsPosition();
        }
    }
}