using UnityEngine;

public abstract class UpgreadeableBuilding : MonoBehaviour, IUpgradeable
{
    public virtual void OpenUpgradeUI()
    {
        print("open ui");
    }
    
    public virtual void Upgrade()
    {
        print("Upgrade not implemented!");
    }

    public virtual bool IsUpgradable()
    {
        return false;
    }
}
