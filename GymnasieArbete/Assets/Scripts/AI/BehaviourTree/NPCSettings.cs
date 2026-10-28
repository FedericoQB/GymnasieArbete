using UnityEngine;

[CreateAssetMenu(menuName = "AI Agent Settings")]
public class NPCSettings : ScriptableObject
{
    // THIS SETTING IS FOR STORING SIMPLE VALUES FOR THE AGENT

    // Perception
    [Header("Echolocation Values")]
    public int rayCount = 9;
    public float rayLength = 10f;
    public float delayInterval = 0.5f;

    // Patrol
    [Header("Patrol Values")]
    public float patrolIdleTime = 2f;
}
