using TMPro;
using UnityEngine;

public class GoldMiner : ActionCard
{
    [SerializeField] private GoldMinerData goldMinerData;
    [SerializeField] private TMP_Text fillAmountText;

    private float _nextMiningTime = float.MinValue;

    private GoldManager _goldManager;

    private int _inBagAmount;

    private void Awake()
    {
        _goldManager = GoldManager.Instance;
    }

    private void Start()
    {
        UpdateFillAmountText();
    }

    private void Update()
    {
        if (Time.time > _nextMiningTime && _inBagAmount < goldMinerData.MaxGoldAmountCanCarry)
        {
            _nextMiningTime = Time.time + 1 / goldMinerData.MineFrequency;
            _inBagAmount++;
            UpdateFillAmountText();
        }
    }

    private void OnMouseDown()
    {
        CollectGolds();
    }

    private void CollectGolds()
    {
        for (var i = 0; i < _inBagAmount; i++)
        {
            MineGold();
        }
        _inBagAmount = 0;
        UpdateFillAmountText();
    }

    private void MineGold()
    {
        var obj = Instantiate(goldMinerData.GoldPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<GoldMoveAnimation>().MoveCoinToPosition(goldMinerData.MoveSpeed, CompletedAction);
    }

    private void CompletedAction()
    {
        _goldManager.CurrentGold += goldMinerData.MiningAmount;
        EventManager.UpdateGoldUI?.Invoke();
    }

    private void UpdateFillAmountText()
    {
        fillAmountText.text = $"{_inBagAmount}/{goldMinerData.MaxGoldAmountCanCarry}";
    }
}