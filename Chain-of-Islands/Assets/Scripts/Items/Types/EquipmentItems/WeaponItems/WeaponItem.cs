using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItem : EquipmentItem
{

    public override ItemType ItemType => ItemType.Weapon;

    public override Type GetItemType() => typeof(WeaponItem);
}
