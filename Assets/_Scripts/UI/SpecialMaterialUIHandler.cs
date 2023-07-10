using TMPro;
using UnityEngine;

public class SpecialMaterialUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text specialMaterialText;
    
    private void OnEnable()
    {
        EventManager.UpdateSpecialMaterialUI += UpdateSpecialMaterial;
    }

    private void OnDisable()
    {
        EventManager.UpdateSpecialMaterialUI -= UpdateSpecialMaterial;
    }
    
    private void UpdateSpecialMaterial()
    {
        specialMaterialText.text = SpecialMaterialManager.Instance.CurrentSpecialMaterial.ToString();
    }
}
