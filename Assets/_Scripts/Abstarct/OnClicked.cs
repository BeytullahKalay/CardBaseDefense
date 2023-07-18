using DG.Tweening;
using UnityEngine;

public abstract class OnClicked : MonoBehaviour,ISelect
{
    public virtual void OnSelected()
    {
        transform.DOPunchScale(Vector3.one * .15f, .1f,2);
    }
}
