public interface IHealthSystem
{
    public int Health { get;}
    public void GetDamage(int damage);
    public void Die();
}