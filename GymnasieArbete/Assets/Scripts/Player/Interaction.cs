using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Interaction : MonoBehaviour
{
    private bool isInRange = false;
    public UnityEvent interactAction; // Is a variable which can be assigned to access a specific function in a script

    void Start()
    {
        GetComponent<SphereCollider>().isTrigger = true; // Makes it available for passthrough
    }

    public void Interact()
    {
        if (isInRange)
        {
            interactAction.Invoke();
        }
    }

    public void InRange()
    {
        isInRange = !isInRange;
    }
}
