using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Echolocation_Node : Node
{
    private int rayCount = 9;
    private float rayLength = 10f;

    private float delayInterval = 2f;
    private float delayCounter;

    private LayerMask layerMask = LayerMask.GetMask("Wall", "Player");

    Transform playerTransform;

    AI_Memory aI_Memory;

    public Echolocation_Node(AI_Base aI_Base, NPCSettings npcSettings) : base(aI_Base, npcSettings)
    {
        rayCount = npcSettings.rayCountEcho;
        rayLength = npcSettings.rayLengthEcho;

        delayInterval = npcSettings.delayInterval;
        delayCounter = delayInterval;

        aI_Memory = aI_Base.GetMemory();
    }

    public override NodeState Evaluate()
    {
        state = Echolocate();
        return state;
    }

    NodeState Echolocate()
    {
        Transform originTransform = aI_Base.transform;

        // Temporary boolean for checking rays
        bool raySeesPlayer = false;

        for (int i = 0; i < rayCount; i++)
        {
            float currentAngle = Mathf.Lerp(0, 360, (float)i / (rayCount - 1)); // Calculates angle for current raycast

            Vector3 rayDirection = Quaternion.AngleAxis(currentAngle, Vector3.up) * originTransform.forward; // Sets angle for the current raycast

            RaycastHit hit;
            if (Physics.Raycast(originTransform.position, rayDirection, out hit, rayLength, layerMask)) // Sends out a Raycast for the player
            {

                if (hit.collider.CompareTag("Player")) // Checks if its the Player
                {
                    raySeesPlayer = true;
                    playerTransform = hit.transform;
                    aI_Memory.SetTarget(playerTransform);
                }
            }
            Debug.DrawRay(originTransform.position, rayDirection * rayLength, Color.orangeRed);
        }

        if (raySeesPlayer)
        {
            return NodeState.SUCCESS;
        }

        aI_Memory.LoseSight();
        return NodeState.FAILURE;

        // Replace with checking if player is moving
    }
}
