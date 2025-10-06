using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Items/Tool")]
public class ToolItem : Item
{
    [Header("ToolItem Properties")]
    public ToolType toolType;

}
