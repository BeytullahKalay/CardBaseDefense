using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCard : Card
{
    private GameObject _panel;
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
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("WORK!!");

        if (!CardSelected)
        {
            print("here");
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Debug.Log("NOT DRAGABLE!");
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OPEN DECISION UI");

        CardSelected = !CardSelected;

        _panel.SetActive(CardSelected);

        if (CardSelected)
        {
            CardSelectManager.Instance.SelectedCards.Add(this);
        }
        else
        {
            UnSelectActions();
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        _overThisUI = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        _overThisUI = false;
    }

    private void Update()
    {
        if (!CardSelected) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnSelectActions();
        }

        if (Input.GetMouseButton(0) && !_overThisUI)
        {
            UnSelectActions();
        }
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
}