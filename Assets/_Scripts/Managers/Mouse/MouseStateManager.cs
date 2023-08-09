
public class MouseStateManager : MonoSingleton<MouseStateManager>
{
    private MouseState _mouseState;
    
    public MouseState MouseState => _mouseState;
    
    public void SetMouseBusyStateTo(MouseState state)
    {
        _mouseState = state;
    }
}
