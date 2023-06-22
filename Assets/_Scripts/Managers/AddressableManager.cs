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
    [SerializeField] private AssetReferenceAudioClip gameMusicAssetReference;
    [SerializeField] private AssetReferenceAudioClip fightMusicAssetReference;
    [SerializeField] private AssetReferenceAudioClip baseShootFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip towerShootFXAssetReference;
    [SerializeField] private AssetReferenceAudioClip warHornFXAssetReference;


    [SerializeField] private RangedData baseTowerData;
    [SerializeField] private RangedData arrowTowerData;

    [SerializeField] private AudioSource gameMusicAudioSource;
    [SerializeField] private AudioSource fightMusicAudioSource;
    [SerializeField] private AudioSource waveCallerAudioSource;
    
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

    }
}
