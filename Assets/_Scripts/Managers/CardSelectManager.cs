using System.Collections.Generic;

public class CardSelectManager : MonoSingleton<CardSelectManager>
{
    public List<ClassicCard> SelectedCards = new List<ClassicCard>();

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