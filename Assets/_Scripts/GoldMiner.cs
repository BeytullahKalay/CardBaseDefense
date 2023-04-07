using DG.Tweening;
using TMPro;
using UnityEngine;

public class GoldMiner : ActionCard
{
    [SerializeField] private GoldMinerData goldMinerData;
    [SerializeField] private TMP_Text fillAmountText;

    private float _nextMiningTime = float.MinValue;

    private int _inBagAmount;

    private void Awake()
    {
        SetNextGoldMineTime();
    }

    private void Start()
    {
        UpdateFillAmountText();
    }

    private void Update()
    {
        if (Time.time > _nextMiningTime && !IsGoldBagFull())
        {
            SetNextGoldMineTime();
            _inBagAmount++;
            UpdateFillAmountText();
        }
        else if(IsGoldBagFull())
        {
            SetNextGoldMineTime();
        }
    }

    private bool IsGoldBagFull()
    {
        return _inBagAmount >= goldMinerData.MaxGoldAmountCanCarry;
    }
    
    private void SetNextGoldMineTime()
    {
        _nextMiningTime = Time.time + 1 / goldMinerData.MineFrequency;
    }

    private void OnMouseDown()
    {
        CollectGolds();
        PlayBouncyEffect();
    }
    
    private void PlayBouncyEffect()
    {
        transform.DOPunchScale(Vector3.one * .2f, .1f);
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
        GoldManager.Instance.CurrentGold += goldMinerData.MiningAmount;
        EventManager.UpdateGoldUI?.Invoke();
        GameManager.Instance.UpdateAllCardsState();
    }

    private void UpdateFillAmountText()
    {
        fillAmountText.text = $"{_inBagAmount}/{goldMinerData.MaxGoldAmountCanCarry}";
    }
}