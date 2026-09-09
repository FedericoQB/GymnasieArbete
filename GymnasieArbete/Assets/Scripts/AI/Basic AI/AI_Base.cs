using UnityEngine;

public class AI_Base : MonoBehaviour
{
    public Transform target;

    private AI_References ai_References;

    private void Awake()
    {
        ai_References = GetComponent<AI_References>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination(target.position);
    }

    // Sets destination for the AI
    void SetDestination(Vector3 pos)
    {
        ai_References.navMeshAgent.SetDestination(pos);

    }
}
