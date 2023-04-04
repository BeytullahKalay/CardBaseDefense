using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Transform baseTransform;
    
    [HideInInspector]public List<Card> Cards;

    public void UpdateAllCardsState()
    {
        foreach (var card in Cards)
        {
            card.UpdateCardState();
        }
    }


    public Transform BaseTransform => baseTransform;
}