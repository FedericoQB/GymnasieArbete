using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform startingPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = startingPosition.position;
    }
}
