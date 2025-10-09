using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public abstract ItemType ItemType { get; }

    [Header("Basic Info")]
    public string itemId;
    public string itemName;
    public int maxStackSize;

    [Header("Visuals")]
    public Sprite icon;
    public GameObject worldPrefab;
}
