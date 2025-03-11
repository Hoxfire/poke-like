using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Transform itemsParent;
    public GameObject itemSlotPrefab;

    public void UpdateUI()
    {

        //Debug.Log("chud");
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }
        float index = -2.5f;
        float indey = 2.5f;
        foreach (ItemData item in inventoryManager.inventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemsParent);
            slot.transform.position = new Vector2 (index, indey);
            Image icon = slot.GetComponentInChildren<Image>();
            icon.sprite = item.Icon;
            
            Button button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => inventoryManager.UseItem(item));

            if (index == 2.5f)
            {
                index = -2.5f;
                indey--;
            }
            else
                index++;

            //Debug.Log(index);
        }
    }
}
