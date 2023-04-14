using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardSpawner : MonoBehaviour
{
   [SerializeField] private GameObject cardPrefab;
   [SerializeField] private Transform cardParent;
   [SerializeField] private List<CardData> _cardDatas = new List<CardData>();

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.C))
      {
         print("card created");
         CreateCard();
      }
   }

   private void CreateCard()
   {
      var randomCardData = _cardDatas[Random.Range(0, _cardDatas.Count)];
      var cardObject = Instantiate(cardPrefab, cardParent);
      
      if (cardObject.TryGetComponent<Card>(out var cardScript))
      {
         cardScript.CardData = randomCardData;
      }
      else
      {
         Debug.LogError("No Card script on " + cardObject.name + " game object!");
      }
      
      EventManager.SetCardsPosition?.Invoke();
   }
}
