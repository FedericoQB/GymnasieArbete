using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class AI_References : MonoBehaviour
{
    // References needed for the base AI
    public NavMeshAgent navMeshAgent;

    public float pathUpdateDelay = 0.2f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
