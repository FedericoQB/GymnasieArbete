using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isEquipped;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetEquippedState()
    {
        return isEquipped;
    }

    public void SetEquippedState(bool state)
    {
        isEquipped = state;
    }
}
