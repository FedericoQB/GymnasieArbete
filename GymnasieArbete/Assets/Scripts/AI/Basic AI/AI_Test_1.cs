using UnityEngine;

public class AI_Test1 : AI_Base
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Updates path from base class
        base.UpdatePath(target.position);
    }
}
