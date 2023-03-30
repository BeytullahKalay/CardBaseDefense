using TMPro;
using UnityEngine;

public class GoldUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;

    private GoldManager _goldManager;

    private void Awake()
    {
        _goldManager = GoldManager.Instance;
    }
    
    private void OnEnable()
    {
        EventManager.UpdateGoldUI += UpdateGoldUI;
    }

    private void OnDisable()
    {
        EventManager.UpdateGoldUI -= UpdateGoldUI;
    }

    private void UpdateGoldUI()
    {
        goldText.text = _goldManager.CurrentGold.ToString();
    }
}
