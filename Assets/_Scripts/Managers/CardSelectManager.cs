using System.Collections.Generic;

public class CardSelectManager : MonoSingleton<CardSelectManager>
{
    public List<ClickCard> SelectedCards = new List<ClickCard>();

    private void OnEnable()
    {
        EventManager.CallTheWave += RemoveAllCardsFromList;
    }

    private void OnDisable()
    {
        EventManager.CallTheWave -= RemoveAllCardsFromList;
    }

    private void RemoveAllCardsFromList()
    {
        SelectedCards.Clear();
    }
}