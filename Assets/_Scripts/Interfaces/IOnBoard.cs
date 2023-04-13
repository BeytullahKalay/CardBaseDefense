
using UnityEngine;
using UnityEngine.AI;

public interface IOnBoard
{
    public BoardStates BoardState { get; set; }
    public Transform BoardedTransform { get; set; }
    public NavMeshAgent NavMeshAgent { get; set; }
    public void RemoveFromSpawnerList();
}
