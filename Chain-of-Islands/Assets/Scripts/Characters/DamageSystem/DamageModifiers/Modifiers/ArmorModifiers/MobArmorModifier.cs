using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobArmorModifier : ArmorModifierBase
{
    [SerializeField] private float _baseArmor = 0;
    public override float ModifyDamage(float incomingDamage)
    {
        float reducedDamage = Mathf.Max(0, incomingDamage - _baseArmor); // защита от отрицательного урона
        return reducedDamage;
    }
}