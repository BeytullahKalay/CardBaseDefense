using UnityEngine;

public class ConstructionBuildingUpgrade : UpgradeableBuilding
{
    [SerializeField] private bool upgradeable;

    public override bool IsUpgradable()
    {
        return upgradeable;
    }
}