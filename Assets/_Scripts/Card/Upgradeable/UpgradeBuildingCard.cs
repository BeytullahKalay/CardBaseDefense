using UnityEngine;

public class UpgradeBuildingCard : MonoBehaviour, IBuildingEffectCard
{
    private GameObject _whatIsBaseBuilding;

    private void Start()
    {
        _whatIsBaseBuilding = GameManager.Instance.BaseTransform.gameObject;
    }

    public void DoEffect(GameObject buildingGameObject)
    {
        if (buildingGameObject.TryGetComponent<IUpgradeable>(out var upgradeable))
        {
            upgradeable.OpenUpgradeUI();
            //upgradeable.Upgrade();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("No IUpgradeable on " + buildingGameObject.name);
        }
    }

    public bool IsPlaceable(GameObject castedObject)
    {
        if (castedObject.TryGetComponent<IUpgradeable>(out var upgradeable))
            return (upgradeable.IsUpgradable() && castedObject != _whatIsBaseBuilding);
        
        return false;
    }
}