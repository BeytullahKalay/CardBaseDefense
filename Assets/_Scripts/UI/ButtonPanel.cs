using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text amountText;

    public Button Button => button;
    public TMP_Text AmountText => amountText;
}
