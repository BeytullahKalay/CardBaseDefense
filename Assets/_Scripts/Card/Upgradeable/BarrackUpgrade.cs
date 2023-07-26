using UnityEngine;

public class BarrackUpgrade : UpgreadeableBuilding
{
    [SerializeField] private bool upgradeable;

    public override void OpenUpgradeUI()
    {
        
    }

    public override void Upgrade()
    {
        print("overrided open ui and select upgrade!");
    }

    public override bool IsUpgradable()
    {
        return upgradeable;
    }
}