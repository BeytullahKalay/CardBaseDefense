using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Transform baseTransform;
    [SerializeField] private Canvas mainCanvas;
    
    public List<ClassicCard> Cards;

    public void UpdateAllCardsState()
    {
        foreach (var card in Cards)
        {
            card.UpdateCardState();
        }
    }


    public Transform BaseTransform => baseTransform;

    public Canvas MainCanvas => mainCanvas;
}