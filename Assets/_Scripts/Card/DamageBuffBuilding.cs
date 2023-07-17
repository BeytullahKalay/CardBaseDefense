using UnityEngine;

public class DamageBuffBuilding : BuffBuilding, IActionCard
{
    [SerializeField] private int damageBuffAmount = 10;

    private void OnEnable()
    {
        EventManager.CallTheWave += IncreaseBuildingDamageInRange;
        EventManager.WaveCompleted += DecreaseBuildingDamageInRange;
    }

    private void OnDisable()
    {
        EventManager.CallTheWave -= IncreaseBuildingDamageInRange;
        EventManager.WaveCompleted -= DecreaseBuildingDamageInRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, buffRadius);
    }

    private void IncreaseBuildingDamageInRange()
    {
        
        var buildings = Physics2D.OverlapCircleAll(transform.position, buffRadius);
        
        if (buildings.Length <= 0) return;
        
        foreach (var building in buildings)
            if (building.TryGetComponent<IDamageIncreaseable>(out var damageIncreaseable))
                damageIncreaseable.IncreaseDamage(damageBuffAmount);
    }

    private void DecreaseBuildingDamageInRange(bool waveCompleted)
    {
        if (!waveCompleted) return;

        var buildings = Physics2D.OverlapCircleAll(transform.position, buffRadius);
        
        if (buildings.Length <= 0) return;
        
        foreach (var building in buildings)
            if (building.TryGetComponent<IDamageIncreaseable>(out var decrease))
                decrease.DecreaseDamage(damageBuffAmount);
    }

    public void Enable(bool state)
    {
        enabled = state;
    }
}
