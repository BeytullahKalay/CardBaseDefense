using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseHealth : HealthSystem
{
    
    
    [Header("Fire Values")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float fireSpawnRadius = 1f;
    


    [Space(10)]
    
    [SerializeField] private GameObject canvasGameObject;

    [SerializeField] private Sprite destroyedSprite;


    public Action OnHeal;
    
    private BasicTower _basicTowerScript;
   
    private List<GameObject> _createdFireList = new List<GameObject>();

    private SpriteRenderer _spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        _basicTowerScript = GetComponent<BasicTower>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateSlider += OnDamageTaken;
        OnHeal += CheckFireObjects;
        OnDead += DeadActions;
    }



    protected override void OnDisable()
    {
        base.OnDisable();
        UpdateSlider -= OnDamageTaken;
        OnHeal -= CheckFireObjects;
        OnDead -= DeadActions;
    }

    private void OnDamageTaken()
    {
        switch (Health)
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

    private void DeleteRandomFireObject()
    {
        var fireObj = _createdFireList[Random.Range(0, _createdFireList.Count)];
        _createdFireList.Remove(fireObj);
        Destroy(fireObj);
    }

    private void CheckFireObjects()
    {
        if (Health > 25 && _createdFireList.Count > 2)
            DeleteRandomFireObject();

        if (Health > 50 && _createdFireList.Count > 1)
            DeleteRandomFireObject();
        
        if (Health > 75 && _createdFireList.Count > 0)
            DeleteRandomFireObject();
    }

    private void DeadActions()
    {
        canvasGameObject.SetActive(false);
        _basicTowerScript.enabled = false;
        _spriteRenderer.sprite = destroyedSprite;
        EventManager.GameOver?.Invoke();
    }
}
