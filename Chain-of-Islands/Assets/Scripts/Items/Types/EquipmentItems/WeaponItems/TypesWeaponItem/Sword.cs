using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Items/Equipment/Weapon/Sword")]
public class Sword : WeaponItem
{
    public override string bodyUseEquipmentConditionName => "isSwordAttack";
    public override string bodyUseEquipmentAnimationName => "SwordAttack";

    public override Type GetItemType() => typeof(Sword);
}
