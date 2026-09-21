using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class AI_Basic_1 : AI_Base
{
    [Header("References")]
    public Transform target;
    public Transform playerTarget;
    public GameObject eyesObject;

    // Ingame logic variables
    float idleTime;
    public float idleStartTime;
    LayerMask layerMask;

    bool seesPlayer;

    // Perception
    [Header("Perception Values")]
    [SerializeField] private float maxAngle = 45f;
    [SerializeField] private int rayCount = 9;
    [SerializeField] private float rayLength = 10f;
    

    bool isOnTarget;

    // States
    private enum AIState
    {
        Idle,
        Patrol,
        Walking,
        Chase
        //Attack,

    }

    [SerializeField] private AIState currentState = AIState.Patrol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idleTime = idleStartTime;

        ai_References.navMeshAgent.updateRotation = true;
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
            case AIState.Chase:
                Chase();
                break;
        }
    }

    // Switches the current state to a new chosen state
    void TransitionToState(AIState newState)
    {
        currentState = newState;
        Debug.Log($"AI state transitioned to state: {newState}");
    }

    // Idle state - Waits for a specified time period
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

    // State to make sure it goes to its target without changing course
    void Walking()
    {
        if (isOnTarget) // If on target, return to idle state
        {
            TransitionToState(AIState.Idle);
            return;
        }
        // Patrol();
    }

    void Chase() // Fix chasing and walking
    {
        UpdatePath(playerTarget.position);
    }

    // Checks for player
    void Perception()
    {
        Vector3 origin = eyesObject.transform.position;

        layerMask = LayerMask.GetMask("Wall", "Player");

        for (int i = 0; i < rayCount; i++)
        {
            float currentAngle = Mathf.Lerp(-maxAngle, maxAngle, (float)i / (rayCount - 1)); // Calculates angle for current raycast

            Vector3 rayDirection = Quaternion.AngleAxis(currentAngle, Vector3.up) * transform.forward; // Sets angle for the current raycast

            RaycastHit hit;
            if (Physics.Raycast(origin, rayDirection, out hit, Mathf.Infinity, layerMask)) // Sends out a Raycast for the player
            {
                if (hit.collider.CompareTag("Player"))
                {
                    seesPlayer = true;
                    Debug.Log("Sees Player");
                    playerTarget = hit.collider.transform;
                    TransitionToState(AIState.Chase);
                }
                
            }
            Debug.DrawRay(origin, rayDirection * rayLength, Color.orangeRed);
        }
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
        if (other.CompareTag("TargetTest") || other.CompareTag("Player"))
        {
            isOnTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TargetTest") || other.CompareTag("Player"))
        {
            isOnTarget = false;
        }
    }
}
