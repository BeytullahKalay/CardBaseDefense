using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour,IPointerUpHandler,IPointerDownHandler,IEndDragHandler ,IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CardData _cardData;

    [Header("Card Values")]
    [SerializeField] private TMP_Text cardCostText;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardDescriptionText;


    private GridLayoutGroup _gridLayoutGroup;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private GameManager _gm;
    private GameObject _createdObject;

    private void Awake()
    {
        _gm = GameManager.Instance;
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
    }

    private void Start()
    {
        cardCostText.text = _cardData.Cost.ToString();
        cardNameText.text = _cardData.CardName;
        cardDescriptionText.text = _cardData.CardDescription;
        _gm.Cards.Add(this);
        UpdateCardState();
    }

    public void UpdateCardState()
    {
        if (IsCardPurchasable())
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }
        else
        {
            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;
        }
    }

    private bool IsCardPurchasable()
    {
        return _gm.CurrentGold >= _cardData.Cost;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("on pointer up");
        if (!MouseOverUI.IsPointerOverUIElement() && _createdObject != null)
        {
            _gm.CurrentGold -= _cardData.Cost;
            _gm.UpdateAllCardsState();
            EventManager.UpdateGoldUI?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            _canvasGroup.blocksRaycasts = true;
            _gridLayoutGroup.SetLayoutHorizontal();
            _gridLayoutGroup.SetLayoutVertical();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if (!MouseOverUI.IsPointerOverUIElement())
        {
            if (_createdObject == null)
            {
                _canvasGroup.alpha = 0f;
                _createdObject = Instantiate(_cardData.ObjectToSpawn);
                _createdObject.transform.position = Utilitles.GetMouseToWorldPos2D();
            }
            else
            {
                _createdObject.transform.position = Utilitles.GetMouseToWorldPos2D();
            }
        }
        else
        {
            _canvasGroup.alpha = 1f;
            Destroy(_createdObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}