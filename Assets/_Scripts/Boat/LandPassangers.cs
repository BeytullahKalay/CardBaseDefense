using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LandPassangers : MonoBehaviour
{
    [SerializeField] private float jumpDuration;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDistance = 3f;
    [SerializeField] private Ease ease;

    private List<IOnBoard> _boardedObjectsList = new List<IOnBoard>();
    
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if(child.TryGetComponent<IOnBoard>(out var t)) _boardedObjectsList.Add(t);
        }
    }

    public async void LandPassenger(Vector3Int detectedGroundPosition)
    {
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

    }
}