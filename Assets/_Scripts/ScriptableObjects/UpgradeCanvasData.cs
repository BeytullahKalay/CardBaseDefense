using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UpgradeCanvas Data")]
public class UpgradeCanvasData : ScriptableObject
{
    [SerializeField] private GameObject upgradeButtonPrefab;
    [SerializeField] private float distanceFromCenter;
    [SerializeField] private float duration;
    [SerializeField] private float offsetAngle = 50;

    
    
    public GameObject UpgradeButtonPrefab => upgradeButtonPrefab;
    public float DistanceFromCenter => distanceFromCenter;
    public float Duration => duration;
    public float OffsetAngle => offsetAngle;
}
