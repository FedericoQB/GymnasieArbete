using System;
using UnityEngine;

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

    public float RemainingDistance(Vector3 destination)
    {
        return !ai_References.navMeshAgent.pathPending ? ai_References.navMeshAgent.remainingDistance : Vector3.Distance(transform.position, destination);
    }

    public float CheckDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }

    public Vector3 CheckDestination()
    {
        return ai_References.navMeshAgent.destination;
    }

    // Fully taken from Unity Documentation SHII WORKS
    protected bool pathComplete()
    {
        return ai_References.navMeshAgent.remainingDistance <= ai_References.navMeshAgent.stoppingDistance;
    }

    public AI_Memory GetMemory()
    {
        return aI_Memory;
    }
}
