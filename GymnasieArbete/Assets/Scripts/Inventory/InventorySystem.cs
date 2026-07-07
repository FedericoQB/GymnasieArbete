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
    [SerializeField] private List<Item> items = new List<Item>();

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

    public void EquipItemLeft(bool IsEquipped) // Figure out a way to know if an Item is equipped or not
    {
        if (IsEquipped) UnEquipHand(rightHand);

        UnEquipHand(leftHand);

        Debug.Log("Equipped Item in Left Hand");
        leftHand.GetComponent<Image>().color = Color.red;
    }

    public void EquipItemRight(bool IsEquipped)
    {
        if (IsEquipped) UnEquipHand(leftHand);

        UnEquipHand(rightHand);

        Debug.Log("Equipped Item in Right Hand");
        rightHand.GetComponent<Image>().color = Color.blue;
    }

    public void UnEquipHand(GameObject hand)
    {
        hand.GetComponent<Image>().color = Color.white; // Add actual function later
    }
}
