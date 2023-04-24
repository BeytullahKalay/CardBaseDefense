using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BaseHealth))]
[RequireComponent(typeof(SpriteRenderer))]
public class BaseFireSpriteController : MonoBehaviour
{
    [Header("Fire Values")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float fireSpawnRadius = 1f;
    [SerializeField] private Sprite destroyedSprite;


    private BaseHealth _baseHealth;
    private List<GameObject> _createdFireList = new List<GameObject>();
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _baseHealth = GetComponent<BaseHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _baseHealth.UpdateSlider += OnDamageTaken;
        _baseHealth.OnHeal += CheckFireObjects;
        _baseHealth.OnDead += ChangeSprite;
    }

    private void OnDisable()
    {
        _baseHealth.UpdateSlider -= OnDamageTaken;
        _baseHealth.OnHeal -= CheckFireObjects;
        _baseHealth.OnDead -= ChangeSprite;
    }

    private void OnDamageTaken()
    {
        switch (_baseHealth.Health)
        {
            case <= 75 when _createdFireList.Count <= 0:
                CreateFireObject();
                return;
            case <= 50 when _createdFireList.Count <= 1:
                CreateFireObject();
                return;
            case <= 25 when  _createdFireList.Count <= 2:
                CreateFireObject();
                return;
        }
    }
    
    private void CreateFireObject()
    {
        var randomPos = Random.insideUnitCircle * fireSpawnRadius + (Vector2)transform.position;
        var obj = Instantiate(firePrefab, randomPos, Quaternion.identity);
        _createdFireList.Add(obj);
    }
    
    private void CheckFireObjects()
    {
        if (_baseHealth.Health > 25 && _createdFireList.Count > 2)
            DeleteRandomFireObject();

        if (_baseHealth.Health > 50 && _createdFireList.Count > 1)
            DeleteRandomFireObject();
        
        if (_baseHealth.Health > 75 && _createdFireList.Count > 0)
            DeleteRandomFireObject();
    }
    
    private void DeleteRandomFireObject()
    {
        var fireObj = _createdFireList[Random.Range(0, _createdFireList.Count)];
        _createdFireList.Remove(fireObj);
        Destroy(fireObj);
    }

    private void ChangeSprite()
    {
        _spriteRenderer.sprite = destroyedSprite;
    }
}
