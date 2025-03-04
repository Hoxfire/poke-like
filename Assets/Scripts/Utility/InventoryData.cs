using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventoryData : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        if (item is IStackable stackableItem)
        {
            ItemData existingItem = items.Find(i => i.Name == item.Name);
            if (existingItem != null && existingItem is IStackable existingStack)
            {
                existingStack.StackSize += stackableItem.StackSize;
            }
            else
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }
    }

    public void UseItem(ItemData item, GameObject user)
    {
        item.Use(user);
        if (item is IConsumable)
        {
            items.Remove(item);
        }
    }
}
