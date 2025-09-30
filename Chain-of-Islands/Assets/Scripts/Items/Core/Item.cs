using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Basic Info")]
    public string itemId;
    public string itemName;

    [Header("Visuals")]
    public Sprite icon;
    public GameObject worldPrefab;

    [Header("Inventory")]
    public int maxStackSize ;
    public ItemType itemType;


}
