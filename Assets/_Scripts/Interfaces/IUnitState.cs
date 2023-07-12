using UnityEngine;

public interface IUnit
{
    public UnitStates UnitStates { get; set; }
    public Vector2 MovePos { get; set; }
}