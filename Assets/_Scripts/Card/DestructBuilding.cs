using UnityEngine;

public class DestructBuilding : MonoBehaviour,IBuildingEffectCard
{
    private GameObject _whatIsBaseBuilding;


    private void Start()
    {
        _whatIsBaseBuilding = GameManager.Instance.BaseTransform.gameObject;
    }

    public void DoEffect(GameObject buildingGameObject)
    {
        if (TryGetComponent<IDestructible>(out var destructible))
        {
            destructible.Destruct();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("No IDestructible on " + buildingGameObject.name);
        }
    }

    public bool IsPlaceable(GameObject castedObject)
    {
        return castedObject != _whatIsBaseBuilding;
    }
}
