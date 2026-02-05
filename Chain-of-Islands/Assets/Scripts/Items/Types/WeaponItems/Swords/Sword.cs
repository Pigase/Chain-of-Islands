using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Items/Weapon/Sword")]
public class Sword : WeaponItem
{
    public override string bodyAttackConditionName => "isSwordAttack";
    public override string bodyAttackAnimationName => "SwordAttack";

    public override Type GetItemType() => typeof(Sword);
}
