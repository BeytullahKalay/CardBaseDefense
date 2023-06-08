public class BaseRusherHealth : HealthSystem
{
    protected override void OnEnable()
    {
        base.OnEnable();

        OnDead += CheckDead;
    }


    protected override void OnDisable()
    {
        base.OnDisable();

        OnDead -= CheckDead;
    }


    private void CheckDead()
    {
        Destroy(gameObject);
    }
}