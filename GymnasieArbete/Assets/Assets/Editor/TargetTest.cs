using System.Collections;
using UnityEngine;

public class TargetTest : MonoBehaviour
{
    [SerializeField] private Transform[] positions;

    int currentPos;
    public float switchDelay;

    public bool rotationEnabled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRotation()
    {
        StartCoroutine(Rotation());
    }

    IEnumerator Rotation()
    {
        do
        {
            NextPosition();
            yield return new WaitForSeconds(switchDelay);
        } while (rotationEnabled);
    }

    void NextPosition()
    {
        if (currentPos >= positions.Length)
        {
            gameObject.transform.position = positions[0].position;
            currentPos = 0;
        }
        else
        {
            gameObject.transform.position = positions[currentPos].position;
            currentPos++;
        }
    }
}
