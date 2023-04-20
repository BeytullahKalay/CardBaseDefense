using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    [SerializeField] private Callable loseCallable;

    private void OnEnable()
    {
        EventManager.GameOver += OpenLoseCallable;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= OpenLoseCallable;
    }

    private void OpenLoseCallable()
    {
        loseCallable.gameObject.SetActive(true);
        loseCallable.Action?.Invoke();
    }
}
