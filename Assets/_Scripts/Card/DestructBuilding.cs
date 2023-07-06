using UnityEngine;
[RequireComponent(typeof(BuildingDetectionOnPlacing))]

public class DestructBuilding : MonoBehaviour,IBuildingEffectCard
{
    private GameObject _whatIsBaseBuilding;


    private void Start()
    {
        _whatIsBaseBuilding = GameManager.Instance.BaseTransform.gameObject;
    }

    public void DoEffect(GameObject buildingGameObject)
    {
        buildingGameObject.GetComponent<IDestructable>().Destruct();
        
        Destroy(gameObject);
    }

    public bool IsPlaceable(GameObject castedObject)
    {
        return castedObject != _whatIsBaseBuilding;
    }
}
