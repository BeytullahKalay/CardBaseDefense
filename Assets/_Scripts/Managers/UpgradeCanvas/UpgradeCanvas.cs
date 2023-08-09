using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeCanvas : MonoBehaviour
{
    [SerializeField] private UpgradeCanvasData _upgradeCanvasData;
    [SerializeField] private List<GameObject> upgradeButtons = new List<GameObject>();

    private float _fanAngle;

    private MouseStateManager _mouseStateManager;

    private void Awake()
    {
        _mouseStateManager = MouseStateManager.Instance;
    }

    private void OnEnable()
    {
        _mouseStateManager.SetMouseBusyStateTo(MouseState.Busy);
    }

    private void OnDisable()
    {
        _mouseStateManager.SetMouseBusyStateTo(MouseState.Available);
    }

    
    // this might be the most ugly thing in the coding world.
    private void Update()
    {
        _mouseStateManager.SetMouseBusyStateTo(MouseState.Busy);
    }

    public void CreateNewButton(Sprite buttonImage, UnityEvent upgradeLogic)
    {
        var button = SpawnObjects();
        SetButtonImage(buttonImage, button);
        SetButtonEvent(upgradeLogic, button);
        SortObjects();
    }

    private void SetButtonEvent(UnityEvent upgradeLogic, GameObject button)
    {
        button.GetComponent<ButtonAndImageHolder>().Button.onClick.AddListener(upgradeLogic.Invoke);
    }

    private void SetButtonImage(Sprite buttonImage, GameObject button)
    {
        button.GetComponent<ButtonAndImageHolder>().Image.sprite = buttonImage;
    }

    private GameObject SpawnObjects()
    {
        var obj = Instantiate(_upgradeCanvasData.UpgradeButtonPrefab, transform);
        upgradeButtons.Add(obj);
        return obj;
    }

    private void SortObjects()
    {
        var buttonCount = upgradeButtons.Count;
        var startAngle = -_upgradeCanvasData.OffsetAngle * (buttonCount - 1) / 2f;

        for (var i = 0; i < upgradeButtons.Count; i++)
        {
            var angle = startAngle + i * _upgradeCanvasData.OffsetAngle + 90;
            var spawnPosition = GetPositionOnCircle(angle);
            upgradeButtons[i].transform.DOMove(spawnPosition, _upgradeCanvasData.Duration);
        }
    }

    private Vector2 GetPositionOnCircle(float angle)
    {
        var x = transform.position.x + _upgradeCanvasData.DistanceFromCenter * Mathf.Cos(angle * Mathf.Deg2Rad);
        var y = transform.position.y + _upgradeCanvasData.DistanceFromCenter * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

    public void ResetUpgradeButtons()
    {
        foreach (var button in upgradeButtons)
            Destroy(button);

        upgradeButtons.Clear();
    }
}