
public class MouseStateManager : MonoSingleton<MouseStateManager>
{
    private MouseState _mouseState;

    private void OnEnable()
    {
        EventManager.SetMouseStateTo += SetMouseStateTo;
    }

    private void OnDisable()
    {
        EventManager.SetMouseStateTo += SetMouseStateTo;
    }
    
    private void SetMouseStateTo(MouseState mouseState)
    {
        _mouseState = mouseState;
    }
}
