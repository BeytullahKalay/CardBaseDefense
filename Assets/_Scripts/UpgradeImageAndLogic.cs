using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UpgradeImageAndLogic
{
    [SerializeField] private string upgradeName;
    
    public Sprite UpgradeImage;
    public UnityEvent UpgradeEvent;
}