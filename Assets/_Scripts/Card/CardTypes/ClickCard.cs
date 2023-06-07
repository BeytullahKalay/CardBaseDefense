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
        _button = _panel.transform.GetChild(0).GetComponent<Button>();
        _button.onClick.AddListener(ButtonTestFunc);
    }

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
        if (!CardSelected) PlayUnSelectionAnimation();

        CloseCardSelectionImage();

        _overThisUI = false;
    }

    private void Update()
    {
        if (!CardSelected) return;

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
        PlayUnSelectionAnimation();
        CloseCardSelectionImage();
        transform.SetSiblingIndex(SiblingIndex);

        _cardSelectManager.SelectedCards.Remove(this);
        EventManager.CloseBottomUI?.Invoke();
    }

    private void ButtonTestFunc()
    {
        print("BUTTON WORKED!");
        
        EventManager.AddThatToCurrentGold?.Invoke(-CardData.Cost);
        Gm.UpdateAllCardsState();
        
        
        _button.gameObject.SetActive(false);

        var createdObject = Instantiate(CardData.ObjectToSpawn);
        var groundScript = createdObject.GetComponent<GroundCreate>();
        groundScript.SetForPlacing(5,CardCompleteActions);
    }

    private void CardCompleteActions()
    {
        DestroyCardFromUI();
        EventManager.SetCardsPosition?.Invoke();
    }
}