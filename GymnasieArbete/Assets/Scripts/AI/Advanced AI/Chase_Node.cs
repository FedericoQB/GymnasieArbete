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
        Transform target = aI_Memory.currentTarget;

        if (aI_Base.GetRemainingDistance(target.position) > 1f)
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
                target.GetComponent<HealthSystem>().ChangeHealth(-1);

                waitCounter = 0f;
                waiting = true;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
