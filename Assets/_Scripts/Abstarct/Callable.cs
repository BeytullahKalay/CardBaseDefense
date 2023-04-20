using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Callable : MonoBehaviour
{
    public Action Action;
    [SerializeField] protected List<Callable> nextAction = new List<Callable>();


    public void CallActions()
    {
        Action?.Invoke();
    }

    protected void CallNextActions()
    {
        foreach (var callable in nextAction)
        {
            callable.CallActions();
        }  
    }
}
