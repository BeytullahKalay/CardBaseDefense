using System.Collections.Generic;
using UnityEngine;

public class UpgradeCanvasManager : MonoSingleton<UpgradeCanvasManager>
{
    [SerializeField] private GameObject upgradeCanvasGameObject;
    private RectTransform _upgradeCanvasRectTransform;
    private UpgradeCanvas upgradeCanvas;

    public GameObject UpgradeCanvasGameObject => upgradeCanvasGameObject;

    private void Awake()
    {
        _upgradeCanvasRectTransform = upgradeCanvasGameObject.GetComponent<RectTransform>();
        upgradeCanvas = upgradeCanvasGameObject.GetComponent<UpgradeCanvas>();
    }

    public void SetUpgradeCanvasPosition(Vector2 pos)
    {
        _upgradeCanvasRectTransform.anchoredPosition = pos;
    }

    public void InitializeUpgradeButton(List<UpgradeImageAndLogic> upgradeImageAndLogics)
    {
        upgradeCanvas.ResetUpgradeButtons();

        foreach (var imageAndLogic in upgradeImageAndLogics)
            upgradeCanvas.CreateNewButton(imageAndLogic.UpgradeImage, imageAndLogic.UpgradeEvent);
    }
}