using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class BottomUIBlockRaycastHandler : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventManager.WaveCompleted += BlockRaycast;
        EventManager.CallTheWave += DontBlockRaycast;
    }

    private void OnDisable()
    {
        EventManager.CallTheWave -= DontBlockRaycast;
        EventManager.WaveCompleted -= BlockRaycast;
    }

    private void DontBlockRaycast()
    {
        _canvasGroup.blocksRaycasts = false;
    }
    
    private void BlockRaycast(bool waveStatus)
    {
        _canvasGroup.blocksRaycasts = true;
    }
}
