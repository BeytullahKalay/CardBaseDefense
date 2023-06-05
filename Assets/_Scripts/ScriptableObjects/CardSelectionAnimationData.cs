using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Card Selection Animation Data")]
public class CardSelectionAnimationData : ScriptableObject
{
    [SerializeField] private float moveUpDistance = 50;
    [SerializeField] private float moveUpDuration = .25f;
    
    public float MoveUpDuration => moveUpDuration;
    public float MoveUpDistance => moveUpDistance;
}