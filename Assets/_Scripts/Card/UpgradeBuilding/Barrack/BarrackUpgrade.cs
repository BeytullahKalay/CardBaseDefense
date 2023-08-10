using UnityEngine;

public class BarrackUpgrade : UpgradeableBuilding
{
    [SerializeField] private bool upgradeable;

    public override bool IsUpgradable()
    {
        return upgradeable;
    }
    
    // using by unity event
    public void IncreaseUnitHealth()
    {
        print("1");
        CloseUpgradeUI();
    }

    // using by unity event
    public void IncreaseUnitDamage()
    {
        print("2");
        CloseUpgradeUI();
    }
    
    // using by unity event
    public void IncreaseUnitAmount()
    {
        print("3");
        CloseUpgradeUI();
    }
}

