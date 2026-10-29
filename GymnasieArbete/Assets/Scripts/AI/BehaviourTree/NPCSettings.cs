using UnityEngine;

[CreateAssetMenu(menuName = "AI Agent Settings")]
public class NPCSettings : ScriptableObject
{
    // THIS SETTING IS FOR STORING SIMPLE VALUES FOR THE AGENT

    [Header("Echolocation Values")]
    public float maxUnAwareAngle = 45f;
    public float maxAwareAngle = 45f;
    public int rayCountVision = 9;
    public float rayLengthVision = 10f;

    // Perception
    [Header("Echolocation Values")]
    public int rayCountEcho = 9;
    public float rayLengthEcho = 10f;
    public float delayInterval = 0.5f;

    // Fear Gauge
    [Header("Fear Gauge Values")]
    public Vector2 fearGauge = new (0f, 10f);

    // Stalking
    [Header("Stalk Values")]
    public float maxDistance = 10f;
    public float minDistance = 7f;

    // Patrol
    [Header("Patrol Values")]
    public float patrolIdleTime = 2f;
}
