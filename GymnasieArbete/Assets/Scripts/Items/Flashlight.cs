using UnityEngine;

public class Flashlight : Tools
{
    [SerializeField] private Vector3 leftHandPosition;
    [SerializeField] private Vector3 rightHandPosition;

    [SerializeField] private Transform flashlightTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashlightTransform.localPosition = rightHandPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
