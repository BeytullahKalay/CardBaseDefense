
using UnityEngine;

public class SoundFXManager : MonoSingleton<SoundFXManager>
{
    [SerializeField] private AudioSource soundFXObject;

    /// <summary>
    /// The entered clip is plays once.
    /// </summary>
    /// <param name="audioClip">clip to plays once </param>
    /// <param name="spawnTransform">spawn position transform</param>
    /// <param name="volume">volume of clip</param>
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume = 1f)
    {
        var audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        var clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    
    /// <summary>
    /// Chosing random clip from entered clip and plays once
    /// </summary>
    /// <param name="audioClips">Clips array</param>
    /// <param name="spawnTransform">spawn position transform</param>
    /// <param name="volume">volume of clip</param>
    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform spawnTransform, float volume = 1f)
    {
        var audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClips[Random.Range(0,audioClips.Length)];
        audioSource.volume = volume;
        audioSource.Play();
        var clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
