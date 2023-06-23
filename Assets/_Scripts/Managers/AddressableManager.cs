using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid) : base(guid){}
}

public class AddressableManager : MonoBehaviour
{
    [Header("General Sound FX")]
    [SerializeField] private AssetReferenceAudioClip gameMusicAssetReference;
    [SerializeField] private AssetReferenceAudioClip fightMusicAssetReference;
    [SerializeField] private AssetReferenceAudioClip baseShootFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip towerShootFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip warHornFXAssetReference;
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

    [Header("Card Data")]
    [SerializeField] private CardData archerTowerCardData;
    [SerializeField] private CardData barracsCardData;
    [SerializeField] private CardData goldMineCardData;
    [SerializeField] private CardData groundMakerCardData;
    [SerializeField] private CardData repairCardData;
    [SerializeField] private CardData trapCardData;
    
    [Header("Placing Sound FX")]
    [SerializeField] private AssetReferenceAudioClip placingAssetReference;
    [SerializeField] private AssetReferenceAudioClip groundAssetReference;

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

        placingAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            archerTowerCardData.PlacingSoundFX = clip.Result;
            goldMineCardData.PlacingSoundFX = clip.Result;
            barracsCardData.PlacingSoundFX = clip.Result;
            repairCardData.PlacingSoundFX = clip.Result;
            trapCardData.PlacingSoundFX = clip.Result;
        };

        groundAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            groundMakerCardData.PlacingSoundFX = clip.Result;
        };

        particleVFXGameObject.LoadAssetAsync<GameObject>().Completed += (obj) =>
        {
            archerTowerCardData.PlacingParticleVFX = obj.Result;
            groundMakerCardData.PlacingParticleVFX = obj.Result;
            goldMineCardData.PlacingParticleVFX = obj.Result;
            barracsCardData.PlacingParticleVFX = obj.Result;
            repairCardData.PlacingParticleVFX = obj.Result;
            trapCardData.PlacingParticleVFX = obj.Result;
        };

        mouseOverCardFXAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            archerTowerCardData.MouseOverClipSoundFX = clip.Result;
            groundMakerCardData.MouseOverClipSoundFX = clip.Result;
            goldMineCardData.MouseOverClipSoundFX = clip.Result;
            barracsCardData.MouseOverClipSoundFX = clip.Result;
            repairCardData.MouseOverClipSoundFX = clip.Result;
            trapCardData.MouseOverClipSoundFX = clip.Result;
        };

    }
}
