using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class BottomUIBlockRaycastHandler : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Spawner _spawner;
    

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _spawner = Spawner.Instance;
    }

    private void Update()
    {
        _canvasGroup.blocksRaycasts = _spawner.WaveCleared;
    }
}
