using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade Condition Data")]
public class UpgradeConditionData : ScriptableObject
{
    [SerializeField] private bool tempCondition;

    public bool TempCondition => tempCondition;
}
