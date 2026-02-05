using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItem : Item
{
    public virtual string bodyAttackConditionName => "isWeaponAttack";
    public virtual string bodyAttackAnimationName => "WeaponAttack";

    [Header("WeaponItem Properties")]

    public string handAttackConditionName;   // "isAttackStoneSword", "isAttackGoldÑlub"
    public int damage;

    public override ItemType ItemType => ItemType.Weapon;

    public override Type GetItemType() => typeof(WeaponItem);
}
