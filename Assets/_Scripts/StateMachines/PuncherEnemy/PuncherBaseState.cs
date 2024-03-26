

public abstract class PuncherBaseState
{
    public abstract void OnEnter(PuncherStateManager stateManager);
    public abstract void OnUpdate(PuncherStateManager stateManager);
    public abstract void OnExit(PuncherStateManager stateManager);
}
