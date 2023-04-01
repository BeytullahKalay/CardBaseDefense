
using UnityEngine;

public class GoldManager : MonoSingleton<GoldManager>
{
    
    public int CurrentGold = 10;

    [SerializeField] private RectTransform goldTextUIRectTransform;

    private void OnEnable()
    {
        EventManager.AddThatToCurrentGold += AddThatToCurrentGold;
    }

    private void OnDisable()
    {
        EventManager.AddThatToCurrentGold -= AddThatToCurrentGold;
    }

    private void Start()
    {
        EventManager.UpdateGoldUI?.Invoke();
    }

    private void AddThatToCurrentGold(int addVal)
    {
        CurrentGold += addVal;
        EventManager.UpdateGoldUI?.Invoke();
    }


    public RectTransform GoldTextUIRectTransform => goldTextUIRectTransform;
}
