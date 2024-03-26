
public abstract class RangedBaseState
{
    public abstract void OnEnter(RangedStateManager stateManager);
    public abstract void OnUpdate(RangedStateManager stateManager);
    public abstract void OnExit(RangedStateManager stateManager);
}
