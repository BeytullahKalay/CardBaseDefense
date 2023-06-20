
using UnityEngine;

public interface IBuildEffectCard
{
    public void DoEffect(GameObject buildGameObject);

    public bool IsPlaceable(GameObject castedObject);
}
