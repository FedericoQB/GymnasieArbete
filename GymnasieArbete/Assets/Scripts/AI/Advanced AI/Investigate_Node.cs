using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Investigate_Node : Node
{
    AI_Memory aI_Memory;

    // Searching Variables
    float searchDuration;
    float searchTimer;
    float searchRadius;

    bool searching;
    bool hasSearchDestination;

    Vector3 searchPosition;
    Vector3 currentSearchDestination;

    // Waiting Variables
    bool waiting;

    float waitTimer;
    float waitDuration = 3f;

    public Investigate_Node(AI_Base aI_Base, NPCSettings npcSettings) : base(aI_Base, npcSettings)
    {
        aI_Memory = aI_Base.GetMemory();
        searchRadius = npcSettings.searchRadius;
    }

    public override NodeState Evaluate()
    {
        // If it doesnt have the last known position, skip over node
        if (!aI_Memory.hasLastKnownPosition)
        {
            searching = false;
            hasSearchDestination = false;
            return NodeState.FAILURE;
        }
            
        Vector3 lastKnownPosition = aI_Memory.lastKnownTargetPosition;

        Debug.Log("Investigating");

        // Agent moves to the last known position of the player
        if (!searching)
        {
            aI_Base.UpdatePath(lastKnownPosition);

            if (aI_Base.GetRemainingDistance(lastKnownPosition) > aI_Base.GetStoppingDistance())
            {
                return NodeState.RUNNING;
            }

            // Adds randomness to the investigation periods
            searchDuration = Random.Range(npcSettings.searchTimeRange.x, npcSettings.searchTimeRange.y);

            searching = true;
            searchTimer = Time.time + searchDuration;

            searchPosition = aI_Memory.lastKnownTargetPosition;
            hasSearchDestination = false;
        }

        // Search under a set time
        if (Time.time < searchTimer)
        {
            if (!hasSearchDestination)
            {
                if (waiting) // Possible Implementation: Add a possibility of doing an echolocation check each wait
                {
                    if (Time.time < waitTimer)
                        return NodeState.RUNNING;

                    waiting = false;
                }
                SearchArea();
            }

            if (hasSearchDestination)
            {
                aI_Base.UpdatePath(currentSearchDestination);

                if (aI_Base.GetRemainingDistance(currentSearchDestination) <= aI_Base.GetStoppingDistance())
                {
                    waiting = true;
                    waitTimer = Time.time + waitDuration;

                    hasSearchDestination = false;
                }
            }

            return NodeState.RUNNING;
        }

        // If it doesnt find the player, it will forget and continue patrolling
        aI_Memory.ForgetTarget();
        searching = false;
        hasSearchDestination = false;

        return NodeState.FAILURE;
    }

    // Function to search the nearby area
    void SearchArea()
    {
        bool foundPosition = false;
        Vector3 walkablePoint = Vector3.zero;

        // Attempts to find possible points in world space
        for (int i = 0; i < 10; i++)
        {
            // Generate a random horizontal offset
            Vector2 randomOffset = Random.insideUnitCircle * searchRadius;

            Vector3 randomPoint = searchPosition +
                new Vector3(randomOffset.x, 0f, randomOffset.y);

            NavMeshHit hit;

            // If the position is confirmed, it will break the loops
            if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
            {
                if (aI_Base.IsDestinationValid(hit.position))
                {
                    walkablePoint = hit.position;
                    foundPosition = true;
                    break;
                }
            }
        }

        // If found, the Agent will update its path accordingly
        if (foundPosition)
        {
            currentSearchDestination = walkablePoint;
            hasSearchDestination = true;
        }
    }


}
