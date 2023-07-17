using UnityEngine;

public class HealBuilding : BuffBuilding, IActionCard
{
    [SerializeField] private int healAmount = 20;
    
    private void OnEnable()
    {
        EventManager.WaveCompleted += Heal;
    }

    private void OnDisable()
    {
        EventManager.WaveCompleted -= Heal;
    }

    private void Heal(bool waveCompleted)
    {
        if (!waveCompleted) return;
        
        var buildings = Physics2D.OverlapCircleAll(transform.position, buffRadius);

        if (buildings.Length <= 0) return;

        foreach (var building in buildings)
            if (building.TryGetComponent<IHealableBuilding>(out var healableBuilding))
                healableBuilding.HealBuilding(healAmount);
    }

    public void Enable(bool state)
    {
        enabled = state;
    }
}