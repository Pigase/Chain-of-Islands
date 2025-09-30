using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Items/Resource")]
public class ResourceItem : Item
{
    [Header("Resource Properties")]
    public ResourceType resourceType;

    public ResourceItem()
    {
        itemType = ItemType.Resource;
    }
}
