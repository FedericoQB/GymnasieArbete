using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Memory : MonoBehaviour
{
    // Stores Variables that the AI picks up during runtime


    // Investigation memory values
    public Vector3 lastSeenTargetPosition { get; set; }
    public Vector3 lastHeardTargetPosition { get; set; }

    public bool hasLastKnownPosition { get; private set; }
    public bool hasLastHeardPosition { get; private set; }

    public SphereCollider hearingCollider;

    public List<SoundInformation> heardSounds;

    // Fear
    [SerializeField, Range(0f, 100f)]
    private float fearGauge = 0f;

    public float fear { get; private set; }

    // Player
    public Transform currentTarget { get; private set; }

    public bool hasTarget => currentTarget != null;

    public bool playerDetected { get; private set; }

    public float lastDetectionTime { get; private set; }

    public bool playerSeesAI;

    public enum AwarenessState
    {
        Unaware,
        Aware
    }

    public AwarenessState currentState = AwarenessState.Unaware;
    public AwarenessState previousState;

    // Switches the current state to a new chosen state
    public void TransitionToState(AwarenessState newState)
    {
        // Only sets when transitioning
        previousState = currentState;
        currentState = newState;
        Debug.Log($"AI state transitioned to state: {newState}");
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            return;

        currentTarget = target;
        lastSeenTargetPosition = target.position;

        hasLastKnownPosition = true;
        playerDetected = true;
        lastDetectionTime = Time.time;
    }

    public void UpdateLastKnownPosition()
    {
        if (currentTarget == null)
            return;

        lastSeenTargetPosition = currentTarget.position;
        lastDetectionTime = Time.time;
    }

    public void HeardPlayer(Vector3 pos)
    {
        hasLastHeardPosition = true;
        lastHeardTargetPosition = pos;
    }



    public void LoseSight()
    {
        playerDetected = false;
        currentTarget = null;
    }

    public void SetFear(float value)
    {
        fearGauge = Mathf.Clamp(value, 0f, 100f);
    }

    public void ForgetTarget()
    {
        currentTarget = null;
        playerDetected = false;
        hasLastKnownPosition = false;
        hasLastHeardPosition = false;
    }

    // Add hearing, through node or memory
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sound"))
        {
            // Gets the reference and stores into a new var to avoid null calls when object gets destroyed
            SoundInformation tempSoundInfo = other.GetComponent<AudioObject>().soundInfo;

            heardSounds.Add(tempSoundInfo);
        }
    }
}
