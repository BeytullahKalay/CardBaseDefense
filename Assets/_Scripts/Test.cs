using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public int numCards;
    public float radius;
    public Image cardPrefab;
    public float fanAngle;
    public Canvas canvas;

    void Start()
    {
        float cardAngleStep = fanAngle / (numCards - 1);
        float startAngle = -(fanAngle / 2f);

        for (int i = 0; i < numCards; i++)
        {
            float cardAngle = startAngle + i * cardAngleStep;
            float x = Mathf.Sin(cardAngle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Cos(cardAngle * Mathf.Deg2Rad) * radius;

            Image card = Instantiate(cardPrefab, canvas.transform);
            card.rectTransform.anchoredPosition = new Vector2(x, y);
            card.rectTransform.rotation = Quaternion.Euler(0f, 0f, -cardAngle);
        }
    }
}