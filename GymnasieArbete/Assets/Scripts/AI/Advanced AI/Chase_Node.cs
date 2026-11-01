using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chase_Node : Node
{
    AI_Memory aI_Memory;

    public Chase_Node(AI_Base aI_Base, NPCSettings npcSettings) : base(aI_Base, npcSettings)
    {
        aI_Memory = aI_Base.GetMemory();
    }

    public override NodeState Evaluate()
    {
        Transform target = aI_Memory.currentTarget;

        if (aI_Base.GetRemainingDistance(target.position) > 0.5f)
        {
            aI_Base.UpdatePath(target.position);
        }
        else
        {
            Debug.Log("Enemy Attacking");
        }

        state = NodeState.RUNNING;
        return state;
    }
}
