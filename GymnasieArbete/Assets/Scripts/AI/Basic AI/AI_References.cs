using UnityEngine;
using UnityEngine.AI;

public class AI_References : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
