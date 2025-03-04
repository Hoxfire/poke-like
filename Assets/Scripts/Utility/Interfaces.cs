using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Icon { get; }
}

public interface IStackable : IInventoryItem
{
    int StackSize { get; set; }
    int MaxStackSize { get; }
}

public interface IEquippable : IInventoryItem
{
    void Equip();
    void Unequip();
}

public interface IConsumable : IInventoryItem
{
    void Consume();
}