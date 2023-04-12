using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LandPassangers : MonoBehaviour
{
    [SerializeField] private List<IOnBoard> boardedObjectsList = new List<IOnBoard>();
    [SerializeField] private float jumpDuration;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDistance = 3f;
    [SerializeField] private Ease ease;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if(child.TryGetComponent<IOnBoard>(out var t)) boardedObjectsList.Add(t);
        }
    }

    public async void LandPassenger(Vector3Int detectedGroundPosition)
    {
        var dir = (detectedGroundPosition - transform.position).normalized;
        var jumpPos = transform.position + dir * jumpDistance;
        
        foreach (var boarded in boardedObjectsList)
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