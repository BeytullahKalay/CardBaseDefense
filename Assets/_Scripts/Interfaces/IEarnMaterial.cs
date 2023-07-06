public interface IEarnMaterial : IDestructable
{
    public int EarnMaterialAmountOnDestruct { get; }
    public void EarnMaterial();
}
