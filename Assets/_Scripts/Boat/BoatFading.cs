using DG.Tweening;
using UnityEngine;

public class BoatFading : MonoBehaviour
{
    [SerializeField] private SpriteRenderer boatRenderer;
    [SerializeField] private float fadeDuration;
    
    public void FadeAndDestroy()
    {
        var alpha = boatRenderer.color.a;
        DOTween.To(() => alpha, x => alpha = x, 0, fadeDuration).OnUpdate(() =>
        {
            var c = boatRenderer.color;
            c.a = alpha;
            boatRenderer.color = c;
        }).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
