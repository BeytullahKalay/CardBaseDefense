using System;

public static class EventManager
{
    public static Action UpdateGoldUI;
    public static Action<int> AddThatToCurrentGold;
    public static Action SetCardsPosition;
    public static Action CallTheWave;
}
