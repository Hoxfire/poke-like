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
            //TMP_Text nameText = slot.GetComponentInChildren<TMP_Text>();
            icon.sprite = item.Icon;
            //nameText.text = item.Name;
            /*
            if (item is IStackable stackable)
            {
                TMP_Text stackText = slot.GetComponentInChildren<TMP_Text>();
                stackText.text = stackable.StackSize.ToString();
            }
            */
            Button button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => inventoryManager.UseItem(item));

            if (index == 2.5f)
            {
                index = -2.5f;
                indey--;
            }
            else
                index++;

            Debug.Log(index);
        }
    }
}
