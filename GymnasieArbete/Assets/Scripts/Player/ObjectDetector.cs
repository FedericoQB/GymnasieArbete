using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private bool disableAfterActivation;
    [SerializeField] private string tagString;

    public UnityEvent detectionAction; // Is a variable which can be assigned to access a specific function in a script

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == tagString)
        {
            detectionAction.Invoke();

            if (disableAfterActivation) gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == tagString)
        {
            detectionAction.Invoke();

            if (disableAfterActivation) gameObject.SetActive(false);
        }
    }
}
