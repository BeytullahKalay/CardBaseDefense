using DG.Tweening;
using TMPro;
using UnityEngine;

public class GoldMiner : MonoBehaviour, IActionCard
{
    [SerializeField] private GoldMinerData goldMinerData;
    [SerializeField] private TMP_Text fillAmountText;

    private int _numberOfGoldCreating;
    private int _numberOfCollectedGolds;

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
        _numberOfGoldCreating = goldMinerData.NumberOfGoldCreating;
        UpdateFillAmountText();
    }

    private void MakeGold(bool isWaveCompleted)
    {
        if (!isWaveCompleted) return;

        _numberOfCollectedGolds = _numberOfGoldCreating;
        UpdateFillAmountText();
    }

    private void OnMouseDown()
    {
        CollectGolds();
    }

    private void PlayBouncyEffect()
    {
        transform.DOPunchScale(Vector3.one * .2f, .1f);
    }

    private void CollectGolds()
    {
        for (var i = 0; i < _numberOfCollectedGolds; i++)
        {
            MineGold();
        }

        if (_numberOfCollectedGolds > 0)
        {
            SoundFXManager.Instance.PlaySoundFXClip(goldMinerData.Clip, transform);
            PlayBouncyEffect();
        }

        _numberOfCollectedGolds = 0;
        UpdateFillAmountText();
    }

    private void MineGold()
    {
        var obj = Instantiate(goldMinerData.GoldPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<GoldMoveAnimation>().MoveCoinToPosition(goldMinerData.CoinMoveSpeed, CompletedAction);
    }

    private void CompletedAction()
    {
        GoldManager.Instance.CurrentGold += _numberOfCollectedGolds;
        EventManager.UpdateGoldUI?.Invoke();
        GameManager.Instance.UpdateAllCardsState();
    }

    private void UpdateFillAmountText()
    {
        fillAmountText.text = $"{_numberOfCollectedGolds}/{_numberOfGoldCreating}";
    }

    public void Enable(bool state)
    {
        this.enabled = state;
    }
    
    public void IncreaseNumberOfGoldCreating(int increasingAmount)
    {
        _numberOfGoldCreating += increasingAmount;
        UpdateFillAmountText();
    }
}