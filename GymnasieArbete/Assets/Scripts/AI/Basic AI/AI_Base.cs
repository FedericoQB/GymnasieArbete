using UnityEngine;

[RequireComponent (typeof(AI_References))]
public class AI_Base : MonoBehaviour
{
    public Transform target;

    private AI_References ai_References;

    private float pathUpdateDeadline;

    private void Awake()
    {
        ai_References = GetComponent<AI_References>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sets destination for the AI with a small delay for optimization
    public void UpdatePath(Vector3 pos)
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + ai_References.pathUpdateDelay;
            ai_References.navMeshAgent.SetDestination(pos);
        }
    }
}
