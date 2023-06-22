using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource buildStateAudioSource;
    [SerializeField] private AudioSource fightStateAudioSource;

    // private void OnEnable()
    // {
    //     EventManager.CallTheWave += FightState;
    //     EventManager.WaveCompleted += BuildState;
    // }
    //
    // private void OnDisable()
    // {
    //     EventManager.CallTheWave -= FightState;
    //     EventManager.WaveCompleted -= BuildState;
    // }

    // private void Start()
    // {
    //     fightStateAudioSource.volume = 0;
    // }

    private void FightState()
    {
        var buildStateVolume = buildStateAudioSource.volume;
        DOTween.To(() => buildStateVolume, x => buildStateVolume = x, 0, 3f).OnUpdate(() =>
        {
            buildStateAudioSource.volume = buildStateVolume;
        }).OnComplete(() =>  buildStateAudioSource.Stop());

        var fightStateVolume = fightStateAudioSource.volume;
        DOTween.To(() => fightStateVolume, x => fightStateVolume = x, 1, .4f).OnUpdate(() =>
        {
            fightStateAudioSource.volume = fightStateVolume;
        }).OnStart(() =>  fightStateAudioSource.Play());
    }

    private void BuildState(bool state)
    {
        if (!state) return;


        var fightStateVolume = fightStateAudioSource.volume;
        DOTween.To(() => fightStateVolume, x => fightStateVolume = x, 0, 3).OnUpdate(() =>
        {
            fightStateAudioSource.volume = fightStateVolume;
        }).OnComplete(() => fightStateAudioSource.Stop());

        var buildStateVolume = buildStateAudioSource.volume;
        DOTween.To(() => buildStateVolume, x => buildStateVolume = x, 1, .4f).OnUpdate(() =>
        {
            buildStateAudioSource.volume = buildStateVolume;
        }).OnStart(()=> buildStateAudioSource.Play());
    }
}