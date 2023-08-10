using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LandPassengers : MonoBehaviour
{
    [SerializeField] private float jumpDuration;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDistance = 3f;
    [SerializeField] private Ease ease;

    private List<IOnBoard> _boardedObjectsList = new List<IOnBoard>();

    private BoatFading _boatFading;

    private bool _landed;

    private bool _allUnitDead;

    public Action AllUnitsDead;
    
    private void Awake()
    {
        _boatFading = GetComponent<BoatFading>();
    }

    public void AddPassengerToList(IOnBoard enemy)
    {
        _boardedObjectsList.Add(enemy);
    }

    private void Update()
    {
        _boardedObjectsList.RemoveAll(enemy => enemy.IsDead());

        if (_boardedObjectsList.Count == 0)
        {
            _boatFading.FadeAndDestroy();
            AllUnitsDead?.Invoke();
            _allUnitDead = true;
        }
        
        if (_landed) return;
        
        foreach (var onBoard in _boardedObjectsList)
        {
            onBoard.BoardedTransform.rotation = Quaternion.identity;
        }
    }

    public async void LandPassenger(Vector3Int detectedGroundPosition)
    {
        if(_allUnitDead) return;
        
        _landed = true;
        var dir = (detectedGroundPosition - transform.position).normalized;
        var jumpPos = transform.position + dir * jumpDistance;
        
        foreach (var boarded in _boardedObjectsList)
        {
            await boarded.BoardedTransform.DOJump(jumpPos, jumpPower, 1, jumpDuration).SetEase(ease)
                .OnComplete(() =>
                {
                    boarded.NavMeshAgent.Warp(jumpPos);
                    boarded.BoardedTransform.SetParent(null);
                    boarded.BoardState = BoardStates.Landed;
                }).AsyncWaitForCompletion();
        }
        _boatFading.FadeAndDestroy();
    }
}