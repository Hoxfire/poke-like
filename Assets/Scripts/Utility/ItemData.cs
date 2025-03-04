using UnityEngine;

public abstract class ItemData : ScriptableObject, IInventoryItem
{
    public string itemName;
    public Sprite icon;

    public string Name => itemName;
    public Sprite Icon => icon;

    public abstract void Use(GameObject user);
}