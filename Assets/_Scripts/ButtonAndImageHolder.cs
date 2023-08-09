using UnityEngine;
using UnityEngine.UI;

public class ButtonAndImageHolder : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    public Button Button => button;
    public Image Image => image;
}