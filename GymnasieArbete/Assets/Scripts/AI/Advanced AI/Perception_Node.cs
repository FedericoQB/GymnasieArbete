using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perception_Node : Node
{
    private Transform eyesTransform;
    private LayerMask layerMask;

    private float angle;

    private float maxUnAwareAngle = 45f;
    private float maxAwareAngle = 45f;
    private int rayCount = 9;
    private float rayLength = 10f;

    Transform playerTransform;

    AI_Memory aI_Memory;


    public Perception_Node(AI_Base aI_Base, NPCSettings npcSettings, Transform eyesTransform) : base(aI_Base, npcSettings)
    {
        this.eyesTransform = eyesTransform;
        maxUnAwareAngle = npcSettings.maxUnAwareAngle;
        maxAwareAngle = npcSettings.maxAwareAngle;
        rayCount = npcSettings.rayCountVision;
        rayLength = npcSettings.rayLengthVision;

        aI_Memory = aI_Base.GetMemory();
    }

    public override NodeState Evaluate()
    {
        state = Perception();
        return state;
    }

    NodeState Perception()
    {
        Vector3 origin = eyesTransform.transform.position;

        layerMask = LayerMask.GetMask("Wall", "Player");

        // Temporary boolean for checking rays
        Transform detectedPlayer = null;

        /*
         * Possible addition to the reactive AI, increasing the Angle when chasing and looking and narrowing it down when patrolling.
         * For a wider "understanding" of its environment
         */

        switch (aI_Memory.currentState)
        {
            case AI_Memory.AwarenessState.Unaware:
                angle = maxUnAwareAngle;
                break;
            case AI_Memory.AwarenessState.Aware:
                angle = maxAwareAngle;
                break;
        }

        for (int i = 0; i < rayCount; i++)
        {
            float currentAngle = Mathf.Lerp(-angle, angle, (float)i / (rayCount - 1)); // Calculates angle for current raycast

            Vector3 rayDirection = Quaternion.AngleAxis(currentAngle, Vector3.up) * eyesTransform.transform.forward; // Sets angle for the current raycast

            RaycastHit hit;
            if (Physics.Raycast(origin, rayDirection, out hit, rayLength, layerMask)) // Sends out a Raycast for the player
            {
                if (hit.collider.CompareTag("Player")) // Checks if its the Player
                {
                    detectedPlayer = hit.collider.transform; // Sets playerTarget to the hit transform
                }
            }
            Debug.DrawRay(origin, rayDirection * rayLength, Color.orangeRed);
        }

        if (detectedPlayer != null)
        {
            aI_Memory.SetTarget(detectedPlayer);
            aI_Memory.TransitionToState(AI_Memory.AwarenessState.Aware);
            aI_Base.AgentLookAt(detectedPlayer.position, npcSettings.rotationSpeed);
            return NodeState.SUCCESS;
        }

        aI_Memory.LoseSight();
        return NodeState.FAILURE;

    }

    
}
