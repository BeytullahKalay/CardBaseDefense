using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CardData _cardData;

    [Header("Card Values")] 
    [SerializeField] private TMP_Text cardCostText;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardDescriptionText;
    

    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private GameManager _gm;
    private GoldManager _goldManager;

    private GameObject _createdObject;

    private void Awake()
    {
        _gm = GameManager.Instance;
        _goldManager = GoldManager.Instance;
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
        return _goldManager.CurrentGold >= _cardData.Cost;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Helpers.IsPointerOverUIElement(LayerMask.NameToLayer("UI")) &&
            !_createdObject.GetComponent<CollisionDetectionOnPlacing>().Collide)
        {
            EventManager.AddThatToCurrentGold?.Invoke(-_cardData.Cost);
            _gm.UpdateAllCardsState();
            _createdObject.GetComponent<CollisionDetectionOnPlacing>().OpenActionsAndDestroyCollisionDetection();
            DestroyCardFromUI();
        }
        else
        {
            ResetCardUI();
            _canvasGroup.blocksRaycasts = true;
            _gridLayoutGroup.SetLayoutHorizontal();
            _gridLayoutGroup.SetLayoutVertical();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if (!Helpers.IsPointerOverUIElement(LayerMask.NameToLayer("UI")))
        {
            if (_createdObject == null)
            {
                _canvasGroup.alpha = 0f;
                _createdObject = Instantiate(_cardData.ObjectToSpawn);
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

    private void FollowMouseOnIntValues(Transform objectToFollow)
    {
        var pos =Vector2Int.RoundToInt(Helpers.GetWorldPositionOfPointer(Helpers.MainCamera));
        objectToFollow.position = new Vector3(pos.x,pos.y,0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }
    
    private void ResetCardUI()
    {
        _canvasGroup.alpha = 1f;
        Destroy(_createdObject);
    }

    private void DestroyCardFromUI()
    {
        _gm.Cards.Remove(this);
        Destroy(gameObject);
    }
}