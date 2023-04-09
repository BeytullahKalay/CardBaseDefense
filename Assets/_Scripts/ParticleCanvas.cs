using DG.Tweening;
using TMPro;
using UnityEngine;

public class ParticleCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text damageTMPText;

    private Pooler _pooler;

    private void Awake()
    {
        _pooler = Pooler.Instance;
    }

    public void PlayTextAnimation(string damageString, Vector3 spawnPosition, float fadeDuration)
    {
        damageTMPText.text = damageString;
        damageTMPText.transform.position = spawnPosition + Vector3.up * .5f;
        damageTMPText.gameObject.SetActive(true);

        damageTMPText.DOFade(0, fadeDuration);
        damageTMPText.transform.DOMoveY(damageTMPText.transform.position.y + 4, fadeDuration).OnComplete(() =>
        {
            _pooler.ParticleTextPool.Release(gameObject);
            damageTMPText.transform.position = Vector3.zero;
            damageTMPText.SetColorAlpha(1);

        });
    }
}