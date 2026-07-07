using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("Hands")]
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    [Header("Items")]
    [SerializeField] private List<Tools> items = new List<Tools>();

    [Header("Keybinds")]
    public KeyCode inventoryKey = KeyCode.Tab;

    [SerializeField] private GameObject inventoryPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            inventoryPrefab.SetActive(!inventoryPrefab.activeInHierarchy);
            CursorMode();
        }
    }

    void InventoryUI()
    {
        if (!inventoryPrefab.activeInHierarchy)
        {
            return;
        }


    }

    void CursorMode()
    {
        if (inventoryPrefab.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Currently the testing phase uses UI to execute the events, will not work in current state as the hands are geometry

    public void EquipItemLeft(int itemIndex) // Figure out a way to know if an Item is equipped or not
    {
        if (items[itemIndex].GetEquippedState() == true) UnEquipHand(rightHand);

        UnEquipHand(leftHand);

        Debug.Log("Equipped Item in Left Hand");
        items[0].enabled = true;
    }

    public void EquipItemRight(int itemIndex)
    {
        if (items[itemIndex].GetEquippedState() == true) UnEquipHand(rightHand);

        UnEquipHand(rightHand);

        Debug.Log("Equipped Item in Right Hand");
        items[0].enabled = true;
    }

    public void UnEquipHand(GameObject hand)
    {
        // Unequip items

        // Temp test with flashlight
        items[0].enabled = false;
    }
}
