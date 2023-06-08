using System;

public static class EventManager
{
    public static Action UpdateGoldUI;
    public static Action UpdateCardUI;
    
    
    public static Action<int> AddThatToCurrentGold;
    public static Action SetCardsPosition;
    public static Action CallTheWave;
    public static Action CheckIsWaveCleared;
    public static Action<bool> WaveCompleted;
    public static Action GameOver;
    
    public static Action CloseBottomUI;
    public static Action<bool> SetBlockRaycastStateTo;
}
