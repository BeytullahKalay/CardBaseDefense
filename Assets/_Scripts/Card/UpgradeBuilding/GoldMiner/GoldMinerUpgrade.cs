using UnityEngine;

[RequireComponent(typeof(GoldMiner))]
public class GoldMinerUpgrade : UpgradeableBuilding
{
    [SerializeField] private bool upgradeable;

    private GoldMiner _goldMiner;

    public override void Awake()
    {
        base.Awake();

        _goldMiner = GetComponent<GoldMiner>();
    }

    public override bool IsUpgradable()
    {
        return upgradeable;
    }
    
    // using by unity event
    public void IncreaseNumberOfCoin(int increasingGoldAmount)
    {
        _goldMiner.IncreaseNumberOfGoldCreating(increasingGoldAmount);
        CloseUpgradeUI();
    }
}