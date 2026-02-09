using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickaxe", menuName = "Items/Tool/Pickaxe")]
public class Pickaxe : ToolItem
{
    public override Type GetItemType() => typeof(Pickaxe);
}
