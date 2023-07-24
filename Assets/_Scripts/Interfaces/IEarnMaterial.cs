public interface IEarnMaterial : IDestructible
{
    public int EarnMaterialAmountOnDestruct { get; }
    public void EarnMaterial();
}
