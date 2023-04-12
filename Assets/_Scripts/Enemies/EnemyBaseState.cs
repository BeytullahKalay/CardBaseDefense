
public abstract class EnemyBaseState
{
    public abstract void OnEnter(RangedEnemyStateManager stateManager);
    public abstract void OnUpdate(RangedEnemyStateManager stateManager);
    public abstract void OnExit(RangedEnemyStateManager stateManager);
}
