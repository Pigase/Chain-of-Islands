using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolItem : EquipmentItem
{
    [Header("ToolItem Properties")]
    public int tier;

    public override ItemType ItemType => ItemType.Tool;

    public override Type GetItemType() => typeof(ToolItem);
}
