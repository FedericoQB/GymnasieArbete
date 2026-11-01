using BehaviourTree;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class Patrol_Node : Node
{
    private Transform[] _patrolPositions;
    private int _currentPatrolStation = 0;
    private Vector3 targetPos;

    private float waitTime = 2f;
    private float waitCounter = 0f;
    private bool waiting = false;

    AI_Memory aI_Memory;

    public Patrol_Node(AI_Base aI_Base, NPCSettings npcSettings, Transform[] patrolPositions) : base(aI_Base, npcSettings)
    {
        _patrolPositions = patrolPositions;
        waitTime = npcSettings.patrolIdleTime;

        aI_Memory = aI_Base.GetMemory();   
    }
    
    public override NodeState Evaluate()
    {
        if (aI_Memory.currentState != AI_Memory.AwarenessState.Unaware) aI_Memory.TransitionToState(AI_Memory.AwarenessState.Unaware);
        aI_Base.SetUpdateRotation(true);

        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime) waiting = false;
        }
        else
        {

            // Gets the position of the current patrol station
            targetPos = _patrolPositions[_currentPatrolStation].position;

            // Updates to the targetPos
            aI_Base.UpdatePath(targetPos);

            

            // Checks if it is on the target
            if (aI_Base.GetRemainingDistance(targetPos) <= 0.5f)
            {
                waitCounter = 0f;
                waiting = true;
                _currentPatrolStation = (_currentPatrolStation + 1 >= _patrolPositions.Length) ? 0 : _currentPatrolStation + 1;
                Debug.Log(_currentPatrolStation);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
