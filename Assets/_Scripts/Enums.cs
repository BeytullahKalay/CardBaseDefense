

public enum BoardStates
{
    OnBoard,
    Landed
}

[System.Serializable]
public enum UnitStates
{
    Idle,
    Move,
    Attack
}

public enum CardType
{
    DragAndDrop,
    ClickCard,
}

public enum MouseState
{
    Available,
    Busy
}
