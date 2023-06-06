using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Card Selection Animation Data")]
public class CardSelectionAnimationData : ScriptableObject
{
    [SerializeField][Range(0,1)] private float scaleUpPercentage = .25f;
    [SerializeField] private float moveUpDuration = .25f;
    
    public float MoveUpDuration => moveUpDuration;
    public float ScaleUpPercentage => scaleUpPercentage;
}