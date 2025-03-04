using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryData inventory;
    public InventoryUI inventoryUI;
    public GameObject inventoryPanel;

    private void Start()
    {
        inventoryUI = inventoryPanel.GetComponent<InventoryUI>();
        inventoryUI.UpdateUI();
    }

    public void AddItem(ItemData item)
    {
        inventory.AddItem(item);
        inventoryUI.UpdateUI();
    }

    public void UseItem(ItemData item)
    {
        inventory.UseItem(item, gameObject);
        inventoryUI.UpdateUI();
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        // Optionally pause the game when inventory is open
        //Time.timeScale = inventoryPanel.activeSelf ? 0 : 1;
    }
}
