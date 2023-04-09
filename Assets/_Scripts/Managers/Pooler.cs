using TMPro;
using UnityEngine.Pool;
using UnityEngine;

public class Pooler : MonoSingleton<Pooler>
{
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject canvasParticleObject;

    public ObjectPool<GameObject> BulletPool { get; private set; }
    public ObjectPool<GameObject> ParticleTextPool { get; private set; }

    
    private void Awake()
    {
        // hmm this is bad...
        
        BulletPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(bulletObject);
        }, bullet =>
        {
            bullet.gameObject.SetActive(true);
        }, bullet =>
        {
            bullet.gameObject.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet.gameObject);
        }, false, 100,250);
        
        
        ParticleTextPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(canvasParticleObject);
        }, canvas =>
        {
            canvas.gameObject.SetActive(true);
        }, canvas =>
        {
            canvas.gameObject.SetActive(false);
        }, canvas =>
        {
            Destroy(canvas.gameObject);
        }, false, 100,250);
    }
}
