using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(AI_References))]
public class AI_Base : MonoBehaviour
{
    protected AI_References ai_References;

    protected float pathUpdateDeadline;

    protected AI_Memory aI_Memory;

    protected virtual void Awake()
    {
        try
        {
            ai_References = GetComponent<AI_References>();
            aI_Memory = GetComponent<AI_Memory>();
        }
        catch (MissingComponentException e)
        {
            Debug.LogError("A required component is missing: " + e);
        }
        catch (Exception e)
        {
            Debug.LogError("An unexpected error occurred: " + e);
        }
    }

    // Sets destination for the AI with a small delay for optimization
    public virtual void UpdatePath(Vector3 pos)
    {
        PauseAgent(false);

        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + ai_References.pathUpdateDelay;
            ai_References.navMeshAgent.SetDestination(pos);
        }
    }

    public virtual void PauseAgent(bool paused)
    {
        ai_References.navMeshAgent.isStopped = paused;
    }

    public float GetRemainingDistance(Vector3 destination)
    {
        return !ai_References.navMeshAgent.pathPending ? ai_References.navMeshAgent.remainingDistance : Vector3.Distance(transform.position, destination);
    }

    public float GetStoppingDistance()
    {
        return ai_References.navMeshAgent.stoppingDistance;
    }

    public float GetDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }

    public Vector3 GetDestination()
    {
        return ai_References.navMeshAgent.destination;
    }

    // Fully taken from Unity Documentation
    protected bool IsPathComplete()
    {
        return ai_References.navMeshAgent.remainingDistance <= ai_References.navMeshAgent.stoppingDistance;
    }

    public bool IsDestinationValid(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();

        bool pathFound = ai_References.navMeshAgent.CalculatePath(destination, path);
        return pathFound && path.status == NavMeshPathStatus.PathComplete;
    }

    public AI_Memory GetMemory()
    {
        return aI_Memory;
    }

    public NavMeshAgent GetNavMeshComponent()
    {
        return ai_References.navMeshAgent;
    }

    public void SetUpdateRotation(bool condition)
    {
        GetNavMeshComponent().autoRepath = condition;
    }

    public void AgentLookAt(Vector3 target, float rotationSpeed)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
