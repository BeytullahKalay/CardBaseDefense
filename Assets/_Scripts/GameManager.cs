
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
    public int CurrentGold = 10;

    public List<Card> Cards;

    public void UpdateAllCardsState()
    {
        foreach (var card in Cards)
        {
            card.UpdateCardState();
        }
    }
}