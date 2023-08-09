using UnityEngine;

public class DamageBuffBuildingUpgrade : UpgradeableBuilding
{
    [SerializeField] private bool upgradeable;

    public override bool IsUpgradable()
    {
        return upgradeable;
    }
}