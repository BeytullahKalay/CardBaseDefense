using System;
using UnityEngine;

public class TrapRepair : MonoBehaviour
{
    public TrapData TrapData { get; set; }
    public Action RepairStuff;

    private void OnMouseDown()
    {
        RepairStuff?.Invoke();
        enabled = false;
    }
}
