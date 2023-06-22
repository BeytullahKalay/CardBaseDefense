using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Gold Miner Data")]
public class GoldMinerData : ScriptableObject
{
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private int maxGoldAmountCanCarry;
    [SerializeField] private int miningAmount = 1;
    [SerializeField] private float mineFrequency = 1;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private AudioClip clip;

    public float MineFrequency => mineFrequency;
    public float MoveSpeed => moveSpeed;
    
    public int MiningAmount => miningAmount;
    public int MaxGoldAmountCanCarry => maxGoldAmountCanCarry;

    public GameObject GoldPrefab => goldPrefab;

    public AudioClip Clip => clip;

}
