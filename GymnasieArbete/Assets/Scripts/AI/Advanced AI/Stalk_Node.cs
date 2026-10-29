using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Stalk_Node : Node
{
    float maxDistance = 10f;
    float minDistance = 8f;

    Transform target;

    AI_Memory aI_Memory;

    public Stalk_Node(AI_Base aI_Base, NPCSettings npcSettings) : base(aI_Base, npcSettings)
    {
        maxDistance = npcSettings.maxDistance;
        minDistance = npcSettings.minDistance;

        aI_Memory = aI_Base.GetMemory();
    }

    public override NodeState Evaluate()
    {
        state = Stalk();
        return state;
    }

    NodeState Stalk()
    {
        target = aI_Memory.currentTarget;

        Debug.Log(target == null);

        if (target == null) return NodeState.FAILURE;

        float distance = aI_Base.CheckDistance(target.position);


        if (distance > maxDistance)
        {
            aI_Base.UpdatePath(target.position);
        }
        else
        {
            aI_Base.PauseAgent(true);
        }

        return NodeState.RUNNING;
    }
}
