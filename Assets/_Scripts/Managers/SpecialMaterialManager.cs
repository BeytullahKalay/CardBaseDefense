
public class SpecialMaterialManager : MonoSingleton<SpecialMaterialManager>
{
    public int CurrentSpecialMaterial = 10;
    
    private void OnEnable()
    {
        EventManager.AddThatToCurrentSpecialMaterial += AddThatToCurrentSpecialMaterial;
    }

    private void OnDisable()
    {
        EventManager.AddThatToCurrentSpecialMaterial -= AddThatToCurrentSpecialMaterial;
    }
    
    private void Start()
    {
        EventManager.UpdateSpecialMaterialUI?.Invoke();
    }
    
    private void AddThatToCurrentSpecialMaterial(int addVal)
    {
        CurrentSpecialMaterial += addVal;
        EventManager.UpdateSpecialMaterialUI?.Invoke();
    }
    
    public bool IsPurchasable(int cost)
    {
        return (CurrentSpecialMaterial >= cost);
    }
}
