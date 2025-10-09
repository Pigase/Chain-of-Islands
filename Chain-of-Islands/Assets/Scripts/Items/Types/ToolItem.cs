using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Items/Tool")]
public class ToolItem : Item, IPickupable
{
    public override ItemType ItemType => ItemType.Tool;

    [Header("ToolItem Properties")]
    public int tier;
}
