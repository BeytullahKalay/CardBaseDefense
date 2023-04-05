using UnityEngine.Pool;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;

    public ObjectPool<GameObject> BulletPool;

    
    private void Awake()
    {
        BulletPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(canvasObject);
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
