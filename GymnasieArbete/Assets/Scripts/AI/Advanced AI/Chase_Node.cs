using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chase_Node : Node
{
    private float waitTime = 2f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public Chase_Node(AI_Base aI_Base, NPCSettings npcSettings) : base(aI_Base, npcSettings)
    {
        aI_Memory = aI_Base.GetMemory();
    }

    public override NodeState Evaluate()
    {
        // Current possible problem, the perception is not catching the player when it is up close and
        // resets the currentTarget will possible make it attack instead - THIS ALSO PROBLEM
        // FOUND PROBLEM, RemainingDistance function breaks after getting up close. It keeps its value, 
        // which in turn leads to continous damage.
        /* 
         * Since the check remaining distance is the cause of updating the path, once it returns false,
         * there is nothing to update the path which in turn never changes the path, aka never change the distance.
         * 
         * FIXED atleast the second problem
         */

        Transform target = aI_Memory.currentTarget;

        Debug.Log($"Target is: {target}");

        // Sets fear to maximum
        aI_Memory.SetFear(100f);

        if (aI_Base.GetRemainingDistance(target.position) > 1f || aI_Base.GetPossiblePathRemainingDistance(target.position) > 1f)
        {
            aI_Base.UpdatePath(target.position);
        }
        else
        {
            if (waiting)
            {
                waitCounter += Time.deltaTime;
                if (waitCounter >= waitTime) waiting = false;
            }
            else
            {
                target.GetComponent<HealthSystem>().TakeDamage(1);

                waitCounter = 0f;
                waiting = true;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
