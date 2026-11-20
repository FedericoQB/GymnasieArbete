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

    public bool hasLastSeenPosition { get; private set; }
    public bool hasLastHeardPosition { get; private set; }

    public SphereCollider hearingCollider;

    public List<SoundInformation> heardSounds;

    // Fear
    [SerializeField, Range(0f, 100f)]
    private float fearGauge = 0f;

    // Player
    public Transform currentTarget { get; private set; }

    // Unused variable
    public bool hasTarget => currentTarget != null;

    public bool playerDetected { get; private set; }

    public float lastDetectionTime { get; private set; }

    //public bool playerSeesAI;

    public float maximumTimeEnemyVisibleToPlayer = 10f;
    float timeEnemyVisibleToPlayer;

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
        Debug.Log($"AI ADVANCED state transitioned to state: {newState}");
    }

    private void Update()
    {
        Debug.Log("Last Heard position: " + lastHeardTargetPosition);
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            return;

        currentTarget = target;
        lastSeenTargetPosition = target.position;

        hasLastSeenPosition = true;
        playerDetected = true;
        lastDetectionTime = Time.time;

        AddFear(10f);
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
        //currentTarget = null;
    }

    public void SetFear(float value)
    {
        fearGauge = Mathf.Clamp(value, 0f, 100f);
    }

    public void AddFear(float value)
    {
        fearGauge += value;
    }

    public float GetFear()
    {
        return fearGauge;
    }

    public void ForgetTarget()
    {
        currentTarget = null;
        playerDetected = false;
        hasLastSeenPosition = false;
        hasLastHeardPosition = false;
    }

    // Add hearing, through node or memory
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sound"))
        {
            // Gets the reference and stores into a new var to avoid null calls when object gets destroyed
            SoundInformation tempSoundInfo = other.GetComponent<AudioObject>().soundInfo;

            if (heardSounds.Exists(sound => sound.soundID == tempSoundInfo.soundID)) return; // If list contains, do not add
            heardSounds.Add(tempSoundInfo);
        }
    }

    // Does player see Enemy
    public bool IsEnemyVisibleToPlayerTimeLimit()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        bool visible = screenPoint.x >= 0 && screenPoint.x <= 1 &&
               screenPoint.y >= 0 && screenPoint.y <= 1 &&
               screenPoint.z > 0;

        if (!visible)
        {
            timeEnemyVisibleToPlayer = 0;
            return false;
        }

        if (timeEnemyVisibleToPlayer < maximumTimeEnemyVisibleToPlayer)
        {
            timeEnemyVisibleToPlayer += Time.deltaTime;
            //Debug.Log("Enemy is NOT visible to player");
            return false;
        }
        else
        {
            //Debug.Log("Enemy is visible to player");

            AddFear(20);

            return true;
        }
    }
}
