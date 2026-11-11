using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
public class Hearing_Node : Node
{


    public Hearing_Node(AI_Base aI_Base, NPCSettings npcSettings) : base(aI_Base, npcSettings)
    {
        aI_Memory = aI_Base.GetMemory();
    }

    public override NodeState Evaluate()
    {
        state = Hearing();
        return state;
    }

    NodeState Hearing()
    {
        // Make hearing turn the AI towards the soudn

        foreach (SoundInformation source in aI_Memory.heardSounds)
        {
            SoundInformation soundInfo = source;

            float distance = Vector3.Distance(aI_Base.transform.position, source.origin);

            float hearingDistance = source.hearingRange;

            if (distance <= hearingDistance)
            {
                // Sends information to memory of known values
                aI_Memory.HeardPlayer(soundInfo.origin);

                // Store the sound's position for the NPC to investigate.
                Debug.Log("Heard Sound: " +  source.audioName + ". At " + aI_Memory.lastHeardTargetPosition);


                return NodeState.SUCCESS;
            }

            // IF it does not hear
            // aI_Memory.heardSounds.Remove(source);
        }

        return NodeState.FAILURE;
    }
}
