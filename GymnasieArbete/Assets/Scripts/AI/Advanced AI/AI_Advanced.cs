using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class AI_Advanced : BTree
{
    public Transform[] waypoints;

    // Perception
    [Header("Perception Values")]
    [SerializeField] private float maxAngle = 45f;
    [SerializeField] private int rayCount = 9;
    [SerializeField] private float rayLength = 10f;

    // Patrol
    [Header("Patrol Values")]
    [SerializeField] private float patrolIdleTime = 2f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                // Perception Node
            }),
            new TaskPatrol(this, waypoints, patrolIdleTime),
        });

        return root;
    }
}
