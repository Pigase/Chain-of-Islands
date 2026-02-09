using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Axe", menuName = "Items/Equipment/Tool/Axe")]
public class Axe : ToolItem
{
    public override string bodyUseEquipmentConditionName => "isCutting";
    public override string bodyUseEquipmentAnimationName => "Cutting";

    public override Type GetItemType() => typeof(Axe);

}
