using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card Data")]
public class CardData : ScriptableObject
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int cost;
    [SerializeField] private string cardName;
    [SerializeField] private CardType cardType;
    [SerializeField] private GameObject buttonPanel;
    [TextArea(5,5)] [SerializeField] private string cardDescription;
    
    
    public GameObject ObjectToSpawn => objectToSpawn;
    public int Cost => cost;
    public string CardName => cardName;
    public string CardDescription => cardDescription;
    public CardType CardType => cardType;
    public GameObject Buttonpanel => buttonPanel;
}
