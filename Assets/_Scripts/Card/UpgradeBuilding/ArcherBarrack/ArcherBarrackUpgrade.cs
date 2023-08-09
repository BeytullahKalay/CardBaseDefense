using UnityEngine;

public class ArcherBarrackUpgrade : UpgradeableBuilding
{
    [SerializeField] private bool upgradeable;

    public override bool IsUpgradable()
    {
        return upgradeable;
    }
}