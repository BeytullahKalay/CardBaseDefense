
using UnityEngine;

public interface IBuildingEffectCard
{
    public void DoEffect(GameObject buildingGameObject);

    public bool IsPlaceable(GameObject castedObject);
}
