using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCard : ClassicCard
{
    private GameObject _panel;
    private Button _button;
    private bool _created;
    private bool _overThisUI;

    private CardSelectManager _cardSelectManager;
    private TMP_Text _groundAmountText;

    private readonly int _placeGroundAmount = 5;
    private bool _cardInAction;

    public override void Awake()
    {
        base.Awake();

        _cardSelectManager = CardSelectManager.Instance;
    }

    public override void Start()
    {
        base.Start();

        _panel = Instantiate(CardData.Buttonpanel, transform);
        _panel.SetActive(false);

        var buttonPanel = _panel.GetComponent<ButtonPanel>();

        _button = buttonPanel.Button;
        _button.onClick.AddListener(ButtonPressedActions);
        _groundAmountText = buttonPanel.AmountText;
        buttonPanel.AmountText.text = "X" + _placeGroundAmount;
    }

    // those empty function(s) should be removed
    public override void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("ON POINTER UP!!");
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Debug.Log("NOT DRAGABLE!");
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        CardSelected = !CardSelected;
        _panel.SetActive(CardSelected);

        if (CardSelected)
            CardSelectManager.Instance.SelectedCards.Add(this);
        else
            UnSelectActions();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        _overThisUI = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!CardSelected)
        {
            PlayUnSelectionAnimation();
            CloseCardSelectionImage();
        }

        _overThisUI = false;
    }

    private void Update()
    {
        if (!CardSelected) return;

        if (_cardInAction) return;

        if (Input.GetKeyDown(KeyCode.Escape))
            UnSelectActions();

        if (Input.GetMouseButton(0) && !_overThisUI)
            UnSelectActions();
    }

    private void UnSelectActions()
    {
        CardSelected = false;
        _panel.SetActive(false);

        // on exit stuff
        transform.SetSiblingIndex(SiblingIndex);
        PlayUnSelectionAnimation();
        CloseCardSelectionImage();

        _cardSelectManager.SelectedCards.Remove(this);
        EventManager.CloseBottomUI?.Invoke();
    }

    private void ButtonPressedActions()
    {
        print("BUTTON WORKED!");

        _cardInAction = true;

        EventManager.AddThatToCurrentGold?.Invoke(-CardData.Cost);
        Gm.UpdateAllCardsState();


        _button.gameObject.SetActive(false);

        var createdObject = Instantiate(CardData.ObjectToSpawn);
        var groundScript = createdObject.GetComponent<GroundCreate>();
        groundScript.PrepareForPlacing(_placeGroundAmount, CardCompleteActions, _groundAmountText,
            CardData.PlacingSoundFX, CardData.PlacingParticleVFX);

        EventManager.CloseBottomUI?.Invoke();
        EventManager.SetBlockRaycastStateTo?.Invoke(false);
    }

    private void CardCompleteActions()
    {
        DestroyCardFromUI();
        EventManager.SetCardsPosition?.Invoke();
    }
}