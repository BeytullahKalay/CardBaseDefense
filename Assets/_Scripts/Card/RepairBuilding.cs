using UnityEngine;


[RequireComponent(typeof(BuildingDetectionOnPlacing))]
public class RepairBuilding : MonoBehaviour, IBuildEffectCard
{
    [SerializeField] private int repairAmount = 100;


    public void DoEffect(GameObject buildGameObject)
    {
        if (buildGameObject.TryGetComponent<HealthSystem>(out var healthSystem))
        {
            healthSystem.Heal(repairAmount);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("No health system on " + buildGameObject.name);
        }
    }

    public bool IsPlaceable(GameObject castedObject)
    {
        if (castedObject.TryGetComponent<HealthSystem>(out var healthSystem))
            return healthSystem.Health < healthSystem.MaxHealth;

        return false;
    }
}