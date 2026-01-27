using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Axe", menuName = "Items/Tool/Axe")]
public class Axe : ToolItem
{
    public override Type GetItemType() => typeof(Axe);

}
