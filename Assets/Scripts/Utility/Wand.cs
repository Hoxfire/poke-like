using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wand", menuName = "Inventory/Wand")]
public class Wand : ItemData, IEquippable
{
    public override void Use(GameObject user)
    {
        Equip();
    }

    public void Equip()
    {
        Debug.Log($"Equipped {itemName}");
    }
    public void Unequip()
    {
        Debug.Log($"Unequipped {itemName}");
    }
}