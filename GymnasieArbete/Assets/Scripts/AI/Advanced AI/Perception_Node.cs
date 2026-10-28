using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perception_Node : Node
{
    private GameObject eyesObject;
    private LayerMask layerMask;

    private float maxUnAwareAngle = 45f;
    private float maxLookingAngle = 45f;
    private int rayCount = 9;
    private float rayLength = 10f;

    public Perception_Node(AI_Base aI_Base,
        GameObject eyesObject, LayerMask layerMask, float maxUnAwareAngle,
        float maxLookingAngle, int rayCount, float rayLength
        ) : base(aI_Base)
    {
        this.eyesObject = eyesObject;
        this.layerMask = layerMask;
        this.maxUnAwareAngle = maxUnAwareAngle;
        this.maxLookingAngle = maxLookingAngle;
        this.rayCount = rayCount;
        this.rayLength = rayLength;
    }

    public override NodeState Evaluate()
    {
        Perception();

        state = NodeState.RUNNING;
        return state;
    }

    void Perception()
    {
        Vector3 origin = eyesObject.transform.position;

        layerMask = LayerMask.GetMask("Wall", "Player");

        // Temporary boolean for checking rays
        bool raySeesPlayer = false;

        /*
         * Possible addition to the reactive AI, increasing the Angle when chasing and looking and narrowing it down when patrolling.
         * For a wider "understanding" of its environment
         */

        /*

        for (int i = 0; i < rayCount; i++)
        {
            float currentAngle = Mathf.Lerp(-maxAngle, maxAngle, (float)i / (rayCount - 1)); // Calculates angle for current raycast

            Vector3 rayDirection = Quaternion.AngleAxis(currentAngle, Vector3.up) * eyesObject.transform.forward; // Sets angle for the current raycast

            RaycastHit hit;
            if (Physics.Raycast(origin, rayDirection, out hit, rayLength, layerMask)) // Sends out a Raycast for the player
            {
                if (hit.collider.CompareTag("Player")) // Checks if its the Player
                {
                    raySeesPlayer = true;
                    playerTarget = hit.collider.transform; // Sets playerTarget to the hit transform
                }
            }
            Debug.DrawRay(origin, rayDirection * rayLength, Color.orangeRed);
        }

        // Sets global value to local value
        playerVisible = raySeesPlayer;
        */
    }
}
