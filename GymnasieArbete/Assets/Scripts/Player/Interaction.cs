using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Interaction : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction; // Is a variable which can be assigned to access a specific function in a script

    void Start()
    {
        GetComponent<SphereCollider>().isTrigger = true; // Makes it available for passthrough
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke(); // Fires the event
            }
        }
    }

    public void Interact()
    {
        if (isInRange)
        {
            interactAction.Invoke();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = false;
        }
    }
}
