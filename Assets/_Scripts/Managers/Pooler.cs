using UnityEngine.Pool;
using UnityEngine;

public class Pooler : MonoSingleton<Pooler>
{
    [SerializeField] private GameObject bulletObject;

    public ObjectPool<GameObject> BulletPool { get; private set; }

    
    private void Awake()
    {
        BulletPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(bulletObject);
        }, text =>
        {
            text.gameObject.SetActive(true);
        }, block =>
        {
            block.gameObject.SetActive(false);
        }, block =>
        {
            Destroy(block.gameObject);
        }, false, 100,250);
    }
}
