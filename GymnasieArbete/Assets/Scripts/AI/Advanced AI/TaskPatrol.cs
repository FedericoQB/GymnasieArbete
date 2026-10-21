using BehaviourTree;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class TaskPatrol : Node
{
    private Transform[] _patrolPositions;
    private int _currentPatrolStation = 0;
    private Vector3 targetPos;

    private float waitTime = 2f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public TaskPatrol(AI_Base ai_Base, Transform[] patrolPositions, float idleTime) : base(ai_Base)
    {
        _patrolPositions = patrolPositions;
        waitTime = idleTime;
    }

    public override NodeState Evaluate()
    {
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
            ai_Base.UpdatePath(targetPos);

            // Checks if it is on the target
            if (ai_Base.RemainingDistance(targetPos) <= 0.5f)
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
