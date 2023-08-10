using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Gold Miner Data")]
public class GoldMinerData : ScriptableObject
{
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private int numberOfGoldCreating;
    [SerializeField] private float coinMoveSpeed = 1f;
    
    [field: SerializeField] public AudioClip Clip { get; set; }

    public float CoinMoveSpeed => coinMoveSpeed;
    
    public int NumberOfGoldCreating => numberOfGoldCreating;

    public GameObject GoldPrefab => goldPrefab;
}
