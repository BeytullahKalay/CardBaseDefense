using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeableBuilding : MonoBehaviour, IUpgradeable
{
    [SerializeField] private List<UpgradeImageAndLogic> upgradeImageAndLogics = new List<UpgradeImageAndLogic>();
    
    private UpgradeCanvasManager _upgradeCanvasManager;
    
    private MouseStateManager _mouseStateManager;

    
    private void Awake()
    {
        _upgradeCanvasManager = UpgradeCanvasManager.Instance;
        _mouseStateManager = MouseStateManager.Instance;
    }

    public virtual void OpenUpgradeUI()
    {
        _upgradeCanvasManager.SetUpgradeCanvasPosition(transform.position);
        _upgradeCanvasManager.InitializeUpgradeButton(upgradeImageAndLogics);
        _upgradeCanvasManager.UpgradeCanvasGameObject.SetActive(true);
    }
    public virtual bool IsUpgradable()
    {
        // in this function will check is upgradeable
        return false;
    }

    public virtual void CloseUpgradeUI()
    {
        _mouseStateManager.SetMouseBusyStateTo(MouseState.Available);
        _upgradeCanvasManager.UpgradeCanvasGameObject.SetActive(false);
    }
}
