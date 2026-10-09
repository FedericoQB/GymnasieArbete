using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent (typeof(AI_References))]
public class AI_Base : MonoBehaviour
{
    protected AI_References ai_References;

    protected float pathUpdateDeadline;

    private void Awake()
    {
        ai_References = GetComponent<AI_References>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sets destination for the AI with a small delay for optimization
    public virtual void UpdatePath(Vector3 pos)
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + ai_References.pathUpdateDelay;
            ai_References.navMeshAgent.SetDestination(pos);
        }
    }

    public float RemainingDistance(Vector3 destination)
    {
        return !ai_References.navMeshAgent.pathPending ? ai_References.navMeshAgent.remainingDistance : Vector3.Distance(transform.position, destination);
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
}
