using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] private float health;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        Debug.Log($"Health changed, Current: {health}, from {health - amount}");
    }

    public float GetHealth() { return health; }
}