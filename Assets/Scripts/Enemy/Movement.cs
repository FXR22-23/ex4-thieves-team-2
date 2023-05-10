using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent agent;
    private NavMeshPath path;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(destination.position);
        path = new NavMeshPath();
        
        NavMesh.CalculatePath(transform.position,
            destination.position, NavMesh.AllAreas, path);
        for (int i = 0; i < path.corners.Length - 1; i++)
        {           
            Debug.DrawLine(path.corners[i],
                path.corners[i + 1], Color.red);
        }
    }
}
