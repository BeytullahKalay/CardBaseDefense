using DG.Tweening;
using TMPro;
using UnityEngine;

public class GoldMiner : MonoBehaviour, IActionCard
{
    [SerializeField] private GoldMinerData goldMinerData;
    [SerializeField] private TMP_Text fillAmountText;

    private int _inBagAmount;

    private void OnEnable()
    {
        EventManager.WaveCompleted += MakeGold;
    }

    private void OnDisable()
    {
        EventManager.WaveCompleted -= MakeGold;
    }

    private void Start()
    {
        UpdateFillAmountText();
    }

    private void MakeGold(bool isWaveCompleted)
    {
        if(!isWaveCompleted) return;
        
        _inBagAmount = goldMinerData.MaxGoldAmountCanCarry;
        UpdateFillAmountText();
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

    public void Enable(bool state)
    {
        this.enabled = state;
    }
}