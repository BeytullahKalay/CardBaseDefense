using UnityEngine;

public abstract class ActionCard: MonoBehaviour
{
    public void Enable(bool state)
    {
        enabled = state;
    }
}