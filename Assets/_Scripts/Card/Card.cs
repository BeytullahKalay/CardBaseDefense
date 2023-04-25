using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public CardData CardData;
    
    
    private Canvas _canvas;

    [Header("Card Values")] 
    [SerializeField] private TMP_Text cardCostText;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardDescriptionText;
    [SerializeField] private Image selectionImage;
    [SerializeField] private Color selectedColor;

    private RectTransform _rectTransform;
    private CanvasGroup _cardCanvasGroup;
    private GameManager _gm;
    private GoldManager _goldManager;
    private GameObject _createdObject;
    private CardPositioner _cardPositioner;

    private int _siblingIndex;

    private void Awake()
    {
        _gm = GameManager.Instance;
        _canvas = _gm.MainCanvas;
        _goldManager = GoldManager.Instance;
        _cardCanvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        selectionImage.color = selectedColor;
        _cardPositioner = GetComponentInParent<CardPositioner>();
    }

    private void Start()
    {
        cardCostText.text = CardData.Cost.ToString();
        cardNameText.text = CardData.CardName;
        cardDescriptionText.text = CardData.CardDescription;
        _gm.Cards.Add(this);
        UpdateCardState();
    }

    public void UpdateCardState()
    {
        if (_goldManager.IsPurchasable(CardData.Cost))
        {
            _cardCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            _cardCanvasGroup.blocksRaycasts = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Helpers.IsPointerOverUIElement(LayerMask.NameToLayer("UI")) &&
            _createdObject.GetComponent<IPlaceable>().Placeable)
        {
            EventManager.AddThatToCurrentGold?.Invoke(-CardData.Cost);
            _gm.UpdateAllCardsState();
            _createdObject.GetComponent<IPlaceable>().PlaceActions();
            DestroyCardFromUI();
        }
        else
        {
            ResetCardUI();
            _cardCanvasGroup.blocksRaycasts = true;
        }
        EventManager.SetCardsPosition?.Invoke();
        _cardPositioner.CanvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        if (!Helpers.IsPointerOverUIElement(LayerMask.NameToLayer("UI")))
        {
            if (_createdObject == null)
            {
                _cardCanvasGroup.alpha = 0f;
                _createdObject = Instantiate(CardData.ObjectToSpawn);
                FollowMouseOnIntValues(_createdObject.transform);
            }
            else
            {
                FollowMouseOnIntValues(_createdObject.transform);
            }
        }
        else
        {
            ResetCardUI();
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OpenSelectionImage();
        _siblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    private void OpenSelectionImage()
    {
        var c = selectionImage.color;
        c.a = 1;
        selectionImage.color = c;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var c = selectionImage.color;
        c.a = 0;
        selectionImage.color = c;

        transform.SetSiblingIndex(_siblingIndex);
    }

    private void FollowMouseOnIntValues(Transform objectToFollow)
    {
        var pos =Vector2Int.RoundToInt(Helpers.GetWorldPositionOfPointer(Helpers.MainCamera));
        objectToFollow.position = new Vector3(pos.x,pos.y,0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _cardCanvasGroup.blocksRaycasts = false;
        _cardPositioner.CanvasGroup.blocksRaycasts = false;
    }
    
    private void ResetCardUI()
    {
        _cardCanvasGroup.alpha = 1f;
        Destroy(_createdObject);
    }

    private void DestroyCardFromUI()
    {
        _gm.Cards.Remove(this);
        gameObject.transform.SetParent(null);
        Destroy(gameObject);
    }
}