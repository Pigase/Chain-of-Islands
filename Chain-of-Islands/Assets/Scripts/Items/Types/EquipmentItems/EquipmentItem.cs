using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public virtual string bodyUseEquipmentConditionName => "isEquipmentUse";
    public virtual string bodyUseEquipmentAnimationName => "EquipmentUse";

    [Header("EquipmentItem Properties")]

    public int damage;
    public string handAttackConditionName;   // "isAttackStoneSword", "isAttackGoldÑlub"

    public override ItemType ItemType => ItemType.Equipment;

    public override Type GetItemType() => typeof(EquipmentItem);
}
