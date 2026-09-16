using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class AI_Basic_1 : AI_Base
{
    public Transform target;
    public Transform playerTarget;

    // Ingame logic variables
    float idleTime;
    public float idleStartTime;
    LayerMask layerMask;
    

    bool isOnTarget;

    // States
    private enum AIState
    {
        Idle,
        Patrol,
        Walking
        //Chase,
        //Attack,

    }

    [SerializeField] private AIState currentState = AIState.Patrol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idleTime = idleStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        Perception();

        switch (currentState)
        {
            case AIState.Idle:
                Idle();
                break;
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Walking:
                Walking();
                break;
        }
    }

    // Switches the current state to a new chosen state
    void TransitionToState(AIState newState)
    {
        currentState = newState;
        Debug.Log($"AI state transitioned to state: {newState}");
    }

    void Idle()
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
            return;
        }

        idleTime = idleStartTime;

        TransitionToState(AIState.Patrol);
    }

    void Patrol()
    {
        UpdatePath(target.position);
    }

    void Walking()
    {
        if (isOnTarget)
        {
            TransitionToState(AIState.Idle);
            return;
        }
        // Patrol();
    }

    void Perception()
    {
        Vector3 direction = (playerTarget.position - transform.position);

        layerMask = LayerMask.GetMask("Wall", "Player");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.CompareTag("Player")) Debug.Log("Sees Player");
            else Debug.Log("Does not see Player");
        }
        Debug.DrawRay(transform.position, direction, Color.orangeRed);
    }

    // Sets destination with optimized delay
    public override void UpdatePath(Vector3 pos)
    {
        if (Time.time >= base.pathUpdateDeadline)
        {
            base.pathUpdateDeadline = Time.time + ai_References.pathUpdateDelay;
            ai_References.navMeshAgent.SetDestination(pos);
            TransitionToState(AIState.Walking);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetTest"))
        {
            isOnTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TargetTest"))
        {
            isOnTarget = false;
        }
    }
}
