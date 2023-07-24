public interface IUpgradeable
{
    public UpgradeConditionData ConditionData { get;}
    public void Upgrade();
    public bool IsUpgradable();
}