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
        EventManager.SetBlockRaycastStateTo += SetBlockRaycastStateTo;
    }

    private void OnDisable()
    {
        EventManager.CallTheWave -= DontBlockRaycast;
        EventManager.WaveCompleted -= BlockRaycast;
        EventManager.SetBlockRaycastStateTo -= SetBlockRaycastStateTo;
    }

    private void DontBlockRaycast()
    {
        _canvasGroup.blocksRaycasts = false;
    }
    
    private void BlockRaycast(bool waveStatus)
    {
        _canvasGroup.blocksRaycasts = waveStatus;
    }

    private void SetBlockRaycastStateTo(bool state)
    {
        _canvasGroup.blocksRaycasts = state;
    }
}
