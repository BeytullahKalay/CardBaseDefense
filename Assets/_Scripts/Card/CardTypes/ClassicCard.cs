using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClassicCard : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IPointerEnterHandler,
    IPointerExitHandler
{
    public CardData CardData;

    [SerializeField] private CardSelectionAnimationData cardSelectionAnimationData;

    private Canvas _canvas;

    [Header("Card Values")]
    [SerializeField] private TMP_Text cardCostText;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardDescriptionText;
    [SerializeField] private Image selectionImage;
    [SerializeField] private Color selectedColor;

    private RectTransform _rectTransform;
    private CanvasGroup _cardCanvasGroup;
    protected GameManager Gm;
    private GoldManager _goldManager;
    private GameObject _createdObject;
    private CardPositioner _cardPositioner;

    protected int SiblingIndex;

    [Header("Animation Values")]
    private Vector3 _cardRotationOnDeck;
    private Tween _scaleTween;
    private Tween _rotateTween;
    protected bool CardSelected;

    public virtual void Awake()
    {
        Gm = GameManager.Instance;
        _canvas = Gm.MainCanvas;
        _goldManager = GoldManager.Instance;
        _cardCanvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        selectionImage.color = selectedColor;
        _cardPositioner = GetComponentInParent<CardPositioner>();
    }

    private void OnEnable()
    {
        EventManager.UpdateCardUI += UpdateCardText;
    }

    private void OnDisable()
    {
        EventManager.UpdateCardUI -= UpdateCardText;
    }

    public virtual void Start()
    {
        UpdateCardText();
        Gm.Cards.Add(this);
        UpdateCardState();
        SiblingIndex = transform.GetSiblingIndex();
    }

    private void UpdateCardText()
    {
        cardCostText.text = CardData.Cost.ToString();
        cardNameText.text = CardData.CardName;
        cardDescriptionText.text = CardData.CardDescription;
    }


    public void UpdateCardState()
    {
        _cardCanvasGroup.blocksRaycasts = _goldManager.IsPurchasable(CardData.Cost);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (!Helpers.IsPointerOverUIElement(LayerMask.NameToLayer("UI")) && _createdObject != null &&
            _createdObject.GetComponent<IUsable>().Usable)
        {
            UseCard();
            CardData.IncreaseCostForThisCard();
            EventManager.UpdateCardUI?.Invoke();
        }
        else
        {
            ResetCardAndCanvasRaycast();
        }

        EventManager.SetCardsPosition?.Invoke();
        _cardPositioner.CanvasGroup.blocksRaycasts = true;
        CardSelected = false;
    }

    private void ResetCardAndCanvasRaycast()
    {
        ResetCardUI();
        _cardCanvasGroup.blocksRaycasts = true;
    }

    private void UseCard()
    {
        EventManager.AddThatToCurrentGold?.Invoke(-CardData.Cost);
        Gm.UpdateAllCardsState();
        _createdObject.GetComponent<IPlaceable>().PlaceActions();
        DestroyCardFromUI();
    }

    public virtual void OnDrag(PointerEventData eventData)
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

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        OpenSelectionImage();
        PlaySelectionAnimation();
        //SiblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    private void OpenSelectionImage()
    {
        var c = selectionImage.color;
        c.a = 1;
        selectionImage.color = c;
    }

    public void AssignCardPositionAndRotation(Vector3 cardRotationOnDeck)
    {
        _cardRotationOnDeck = cardRotationOnDeck;
    }

    private void PlaySelectionAnimation()
    {
        _scaleTween?.Kill();
        _rotateTween?.Kill();

        _scaleTween = transform.DOScale(Vector3.one + Vector3.one * cardSelectionAnimationData.ScaleUpPercentage,
            cardSelectionAnimationData.MoveUpDuration);
        _rotateTween = transform.DOLocalRotate(Vector3.zero, cardSelectionAnimationData.MoveUpDuration);
    }

    protected void PlayUnSelectionAnimation()
    {
        _scaleTween?.Kill();
        _rotateTween?.Kill();

        _scaleTween = transform.DOScale(Vector3.one, cardSelectionAnimationData.MoveUpDuration);
        _rotateTween = transform.DOLocalRotate(_cardRotationOnDeck, cardSelectionAnimationData.MoveUpDuration);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.SetSiblingIndex(SiblingIndex);
        PlayUnSelectionAnimation();
        CloseCardSelectionImage();
    }

    protected void CloseCardSelectionImage()
    {
        var c = selectionImage.color;
        c.a = 0;
        selectionImage.color = c;
    }

    private void FollowMouseOnIntValues(Transform objectToFollow)
    {
        var pos = Vector2Int.RoundToInt(Helpers.GetWorldPositionOfPointer(Helpers.MainCamera));
        objectToFollow.position = new Vector3(pos.x, pos.y, 0);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        _cardCanvasGroup.blocksRaycasts = false;
        _cardPositioner.CanvasGroup.blocksRaycasts = false;
        CardSelected = true;
    }

    private void ResetCardUI()
    {
        _cardCanvasGroup.alpha = 1f;
        Destroy(_createdObject);
    }

    protected void DestroyCardFromUI()
    {
        Gm.Cards.Remove(this);
        gameObject.transform.SetParent(null);
        Destroy(gameObject);
    }
}