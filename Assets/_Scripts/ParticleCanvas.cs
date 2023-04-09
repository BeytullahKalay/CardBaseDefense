using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ParticleCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text damageTMPText;


    // private void Start()
    // {
    //     print("Called");
    //     gameObject.SetActive(false);
    // }

    public void PlayTextAnimation(string damageString, Vector3 spawnPosition, float fadeDuration,
        Action animationCompletedActions)
    {
        gameObject.SetActive(true);
        damageTMPText.text = damageString;
        damageTMPText.transform.position = spawnPosition + Vector3.up * .5f;
        damageTMPText.gameObject.SetActive(true);
        damageTMPText.transform.DOMoveY(damageTMPText.transform.position.y + 4, fadeDuration).OnComplete(() =>
        {
            gameObject.SetActive(false);
            damageTMPText.transform.position = spawnPosition;
            animationCompletedActions?.Invoke();
        });
    }
}