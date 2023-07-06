using UnityEngine;


[RequireComponent(typeof(BuildingDetectionOnPlacing))]
public class RepairBuilding : MonoBehaviour, IBuildingEffectCard
{
    [SerializeField] private int repairAmount = 100;


    public void DoEffect(GameObject buildingGameObject)
    {
        if (buildingGameObject.TryGetComponent<HealthSystem>(out var healthSystem))
        {
            healthSystem.Heal(repairAmount);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("No health system on " + buildingGameObject.name);
        }
    }

    public bool IsPlaceable(GameObject castedObject)
    {
        if (castedObject.TryGetComponent<HealthSystem>(out var healthSystem))
            return healthSystem.Health < healthSystem.MaxHealth;

        return false;
    }
}