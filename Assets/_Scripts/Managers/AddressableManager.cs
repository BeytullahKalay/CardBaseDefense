using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid) : base(guid) { }
}

public class AddressableManager : MonoBehaviour
{
    [Header("General Sound FX")]
    [SerializeField] private AssetReferenceAudioClip gameMusicAssetReference;
    [SerializeField] private AssetReferenceAudioClip warHornFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip fightMusicAssetReference;
    [SerializeField] private AssetReferenceAudioClip baseShootFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip towerShootFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip destructionFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip collectCoinFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip mouseOverCardFXAssetReference;

    [Header("Ranged Data")]
    [SerializeField] private RangedData baseTowerData;
    [SerializeField] private RangedData arrowTowerData;
    [SerializeField] private GoldMinerData goldMinerData;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource gameMusicAudioSource;
    [SerializeField] private AudioSource fightMusicAudioSource;
    [SerializeField] private AudioSource waveCallerAudioSource;

    [Header("Building Health Systems")]
    [SerializeField] private BuildingHealthSystem arrowTowerHealthSystem;
    [SerializeField] private BuildingHealthSystem goldMinerHealthSystem;
    [SerializeField] private BuildingHealthSystem barracksHealthSystem;
    [SerializeField] private SupportBuildingHealth damageBuffBuildingHealthSystem;
    [SerializeField] private SupportBuildingHealth constructionBuildingHealthSystem;


    [Header("Card Data")]
    [SerializeField] private CardData archerTowerCardData;
    [SerializeField] private CardData barracsCardData;
    [SerializeField] private CardData barracsArcherCardData;
    [SerializeField] private CardData goldMineCardData;
    [SerializeField] private CardData groundMakerCardData;
    [SerializeField] private CardData repairCardData;
    [SerializeField] private CardData trapCardData;
    [SerializeField] private CardData destructorCardData;
    [SerializeField] private CardData healConstructionCardData;
    [SerializeField] private CardData damageBuffCardData;
    [SerializeField] private CardData upgradeCardData;

    [Header("Placing Sound FX")]
    [SerializeField] private AssetReferenceAudioClip placingAssetReference;
    [SerializeField] private AssetReferenceAudioClip groundAssetReference;
    [SerializeField] private AssetReferenceAudioClip upgradeAssetReference;

    [Header("Placing Particles")]
    [SerializeField] private AssetReferenceGameObject particleVFXGameObject;



    private void Start()
    {
        Addressables.InitializeAsync().Completed += AddressableManager_Completed;
    }

    private void AddressableManager_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        gameMusicAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            gameMusicAudioSource.clip = clip.Result;
            gameMusicAudioSource.Play();
        };

        fightMusicAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            fightMusicAudioSource.clip = clip.Result;
        };

        baseShootFXAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            baseTowerData.ShootClip = clip.Result;
        };

        towerShootFXAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            arrowTowerData.ShootClip = clip.Result;
        };

        warHornFXAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            waveCallerAudioSource.clip = clip.Result;
        };

        collectCoinFXAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            goldMinerData.Clip = clip.Result;
        };

        destructionFXAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            constructionBuildingHealthSystem.DestructionAudioClip = clip.Result;
            damageBuffBuildingHealthSystem.DestructionAudioClip = clip.Result;
            arrowTowerHealthSystem.DestructionAudioClip = clip.Result;
            goldMinerHealthSystem.DestructionAudioClip = clip.Result;
            barracksHealthSystem.DestructionAudioClip = clip.Result;
        };

        placingAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            healConstructionCardData.PlacingSoundFX = clip.Result;
            barracsArcherCardData.PlacingSoundFX = clip.Result;
            archerTowerCardData.PlacingSoundFX = clip.Result;
            damageBuffCardData.PlacingSoundFX = clip.Result;
            goldMineCardData.PlacingSoundFX = clip.Result;
            barracsCardData.PlacingSoundFX = clip.Result;
            repairCardData.PlacingSoundFX = clip.Result;
            trapCardData.PlacingSoundFX = clip.Result;
        };

        groundAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            groundMakerCardData.PlacingSoundFX = clip.Result;
        };

        upgradeAssetReference.LoadAssetAsync<AudioClip>().Completed += clip =>
        {
            upgradeCardData.PlacingSoundFX = clip.Result;
        };

        particleVFXGameObject.LoadAssetAsync<GameObject>().Completed += (obj) =>
        {
            healConstructionCardData.PlacingParticleVFX = obj.Result;
            barracsArcherCardData.PlacingParticleVFX = obj.Result;
            archerTowerCardData.PlacingParticleVFX = obj.Result;
            groundMakerCardData.PlacingParticleVFX = obj.Result;
            destructorCardData.PlacingParticleVFX = obj.Result;
            damageBuffCardData.PlacingParticleVFX = obj.Result;
            goldMineCardData.PlacingParticleVFX = obj.Result;
            barracsCardData.PlacingParticleVFX = obj.Result;
            upgradeCardData.PlacingParticleVFX = obj.Result;
            repairCardData.PlacingParticleVFX = obj.Result;
            trapCardData.PlacingParticleVFX = obj.Result;


            constructionBuildingHealthSystem.DestructParticleVFX = obj.Result;
            damageBuffBuildingHealthSystem.DestructParticleVFX = obj.Result;
            arrowTowerHealthSystem.DestructParticleVFX = obj.Result;
            goldMinerHealthSystem.DestructParticleVFX = obj.Result;
            barracksHealthSystem.DestructParticleVFX = obj.Result;
        };

        mouseOverCardFXAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            healConstructionCardData.MouseOverClipSoundFX = clip.Result;
            barracsArcherCardData.MouseOverClipSoundFX = clip.Result;
            archerTowerCardData.MouseOverClipSoundFX = clip.Result;
            groundMakerCardData.MouseOverClipSoundFX = clip.Result;
            destructorCardData.MouseOverClipSoundFX = clip.Result;
            damageBuffCardData.MouseOverClipSoundFX = clip.Result;
            goldMineCardData.MouseOverClipSoundFX = clip.Result;
            barracsCardData.MouseOverClipSoundFX = clip.Result;
            upgradeCardData.MouseOverClipSoundFX = clip.Result;
            repairCardData.MouseOverClipSoundFX = clip.Result;
            trapCardData.MouseOverClipSoundFX = clip.Result;
        };
    }
}
