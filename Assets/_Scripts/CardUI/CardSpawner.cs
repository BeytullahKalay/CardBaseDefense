using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private int cardAmountOnStart = 3;
    [SerializeField] private GameObject classicCardPrefab;
    [SerializeField] private GameObject clickCardPrefab;
    [SerializeField] private Transform cardParent;
    [SerializeField] private List<CardData> _cardDatas = new List<CardData>();

    private void OnEnable()
    {
        EventManager.WaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        EventManager.WaveCompleted -= WaveCompleted;
    }

    private void Start()
    {
        for (var i = 0; i < cardAmountOnStart; i++)
        {
            CreateCard();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateCard();
        }
    }

    private void CreateCard()
    {
        var randomCardData = _cardDatas[Random.Range(0, _cardDatas.Count)];

        var cardObject = Instantiate(
            randomCardData.CardType == CardType.ClickCard ? clickCardPrefab : classicCardPrefab, cardParent);
        
        if (cardObject.TryGetComponent<Card>(out var cardScript))
            cardScript.CardData = randomCardData;
        else
            Debug.LogError("No Card script on " + cardObject.name + " game object!");

        EventManager.SetCardsPosition?.Invoke();
    }

    private void WaveCompleted(bool isCompleted)
    {
        if (!isCompleted) return;
        CreateCard();
    }
}