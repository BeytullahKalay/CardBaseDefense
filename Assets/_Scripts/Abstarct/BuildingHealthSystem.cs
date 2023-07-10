using UnityEngine;

public class BuildingHealthSystem : HealthSystem,IEarnMaterial
{
    public AudioClip DestructionAudioClip;
    public GameObject DestructParticleVFX;
    
    
    [field: SerializeField] public int EarnMaterialAmountOnDestruct { get; private set; }


    protected override void OnEnable()
    {
        base.OnEnable();
        
        OnDead += PlayDestructionClip;
        OnDead += SpawnParticles;
        OnDead += Destroy;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        OnDead -= PlayDestructionClip;
        OnDead -= SpawnParticles;
        OnDead -= Destroy;
    }

    private void PlayDestructionClip()
    {
        SoundFXManager.Instance.PlaySoundFXClip(DestructionAudioClip, transform);
    }
    
    public void Destruct()
    {
        OnDead?.Invoke();
        EarnMaterial();
    }

    public void EarnMaterial()
    {
        print("Earned " + EarnMaterialAmountOnDestruct + " material(s) from " + gameObject.name);

        EventManager.AddThatToCurrentSpecialMaterial?.Invoke(EarnMaterialAmountOnDestruct);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    
    private void SpawnParticles()
    {
        Instantiate(DestructParticleVFX, transform.position, Quaternion.identity);
    }
}