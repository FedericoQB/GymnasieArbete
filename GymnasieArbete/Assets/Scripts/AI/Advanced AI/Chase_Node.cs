using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chase_Node : Node
{
    public Chase_Node(AI_Base aI_Base, NPCSettings npcSettings) : base(aI_Base, npcSettings)
    {

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("Target");

        if (aI_Base.RemainingDistance(target.position) > 0.5f)
        {
            aI_Base.UpdatePath(target.position);
        }

        state = NodeState.RUNNING;
        return state;
    }
}
