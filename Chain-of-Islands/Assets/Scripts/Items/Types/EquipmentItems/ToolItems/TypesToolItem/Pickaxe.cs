using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickaxe", menuName = "Items/Equipment/Tool/Pickaxe")]
public class Pickaxe : ToolItem
{
    public override string bodyUseEquipmentConditionName => "isMining";
    public override string bodyUseEquipmentAnimationName => "Mining";

    public override Type GetItemType() => typeof(Pickaxe);
}
