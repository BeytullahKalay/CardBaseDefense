using UnityEngine;

public class ConstructionBuilding : MonoBehaviour, IActionCard
{
    [SerializeField] private float constructionRadius = 3f;
    [SerializeField] private int healAmount = 20;

    private void OnEnable()
    {
        EventManager.WaveCompleted += Heal;
    }

    private void OnDisable()
    {
        EventManager.WaveCompleted -= Heal;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, constructionRadius);
    }

    private void Heal(bool waveCompleted)
    {
        if (!waveCompleted) return;
        
        var buildings = Physics2D.OverlapCircleAll(transform.position, constructionRadius);

        if (buildings.Length <= 0) return;

        foreach (var building in buildings)
            if (building.TryGetComponent<IHealableBuilding>(out var healableBuilding))
                healableBuilding.Heal(healAmount);
    }

    public void Enable(bool state)
    {
        enabled = state;
    }
}