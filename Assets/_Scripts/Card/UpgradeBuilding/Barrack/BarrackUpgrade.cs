using UnityEngine;

public class BarrackUpgrade : UpgradeableBuilding
{
    [SerializeField] private bool upgradeable;

    public override bool IsUpgradable()
    {
        return upgradeable;
    }
    
    public void Upgrade1()
    {
        print("1");
    }
    
    public void Upgrade2()
    {
        print("2");
    }
    
    public void Upgrade3()
    {
        print("3");
    }
}

