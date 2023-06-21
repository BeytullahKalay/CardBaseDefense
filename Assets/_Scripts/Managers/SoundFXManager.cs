
using UnityEngine;

public class SoundFXManager : MonoSingleton<SoundFXManager>
{
    [SerializeField] private AudioSource soundFXObject;

    // public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    // {
    //     AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
    //     audioSource.clip = audioClip;
    //     audioSource.volume = volume;
    //     audioSource.Play();
    //     float clipLength = audioSource.clip.length;
    //     Destroy(audioSource.gameObject, clipLength);
    // }
    
    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClips[Random.Range(0,audioClips.Length)];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
