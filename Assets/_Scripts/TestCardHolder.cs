using UnityEditor.Rendering;
using UnityEngine;

public class TestCardHolder : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float fanAngle;
    //[SerializeField] private float fanSpacing;

    void Start()
    {
        SetCardsPosition();
    }

    private void SetCardsPosition()
    {
        var allChildren = transform.GetAllChildren();

        var cardAngleStep = fanAngle / (allChildren.Count - 1);
        var startAngle = -(fanAngle / 2f);


        for (int i = 0; i < allChildren.Count; i++)
        {
            var cardAngle = startAngle + i * cardAngleStep;
            var x = Mathf.Sin(cardAngle * Mathf.Deg2Rad) * radius;
            var y = Mathf.Cos(cardAngle * Mathf.Deg2Rad) * radius;


            allChildren[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            allChildren[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, -cardAngle);
            
        }


        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, + Mathf.Sin(radius * Mathf.Deg2Rad), 0);
    }

    private void OnValidate()
    {
        SetCardsPosition();
    }
}