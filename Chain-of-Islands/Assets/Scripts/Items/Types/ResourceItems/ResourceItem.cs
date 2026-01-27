using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Items/Resource")]
public class ResourceItem : Item
{
    public override ItemType ItemType => ItemType.Resource;

    [Header("Resource Properties")]
    public bool canBeUsedInCrafting;
    public bool canBeUsedInBuilding;

    public override Type GetItemType() => typeof(ResourceItem);

}
