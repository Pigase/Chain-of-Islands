using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Basic Info")]
    public string itemId;
    public string itemName;

    [Header("Visuals")]
    public Sprite icon;
    public GameObject worldPrefab;

    public ItemType GetItemType()
    {
        return this switch
        {
            ResourceItem => ItemType.Resource,
            WeaponItem => ItemType.Weapon,
            ToolItem => ItemType.Tool,
            ArmorItem => ItemType.Armor,
            _ => throw new System.Exception($"Unknown item type: {GetType()}")
        };
    }
}
