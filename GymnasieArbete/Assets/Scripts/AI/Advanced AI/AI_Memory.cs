using UnityEngine;

public class AI_Memory : MonoBehaviour
{
    // Stores Variables that the AI picks up during runtime

    public Vector3 lastKnownTargetPosition { get; set; }

    public bool hasLastKnownPosition { get; private set; }

    [SerializeField, Range(0f, 100f)]
    private float fearGauge = 0f;

    public float fear { get; private set; }

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
        lastKnownTargetPosition = target.position;

        hasLastKnownPosition = true;
        playerDetected = true;
        lastDetectionTime = Time.time;
    }

    public void UpdateLastKnownPosition()
    {
        if (currentTarget == null)
            return;

        lastKnownTargetPosition = currentTarget.position;
        lastDetectionTime = Time.time;
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
    }
}
