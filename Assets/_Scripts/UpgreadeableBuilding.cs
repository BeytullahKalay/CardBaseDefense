using UnityEngine;

public class UpgreadeableBuilding : MonoBehaviour, IUpgradeable
{
    [field: SerializeField] public UpgradeConditionData ConditionData { get; private set; }


    public void Upgrade()
    {
        print("Upgrade not implemented!");
    }

    public bool IsUpgradable()
    {
        print("upgrade condition not implemented!");
        return ConditionData.TempCondition;
    }
}
