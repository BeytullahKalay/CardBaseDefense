using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoldMoveAnimation : MonoBehaviour
{
    private Tweener _tweener;
    private float _moveSpeed = 2f;
    private bool _startMove;
    private Action _completedAction;

    public void MoveCoinToPosition(float moveSpeed, Action completedAction)
    {
        transform.DOMove((Vector2)transform.position + Random.insideUnitCircle * 2, .5f)
            .OnComplete(() => _startMove = true);

        _completedAction = completedAction;
        _moveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!_startMove) return;

        
        var movePos = Helpers.GetWorldPositionOfCanvasElement(GoldManager.Instance.GoldTextUIRectTransform, Helpers.MainCamera);
        if (Vector2.Distance(transform.position, movePos) > .5f)
        {
            var direction = (movePos - (Vector2)transform.position).normalized;
            var pos =direction * (_moveSpeed * Time.fixedDeltaTime);
            transform.position += new Vector3(pos.x,pos.y,0);
        }
        else
        {
            _completedAction?.Invoke();
            Destroy(gameObject);
        }
    }
}