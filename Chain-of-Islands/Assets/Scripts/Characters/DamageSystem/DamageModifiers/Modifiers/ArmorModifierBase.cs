using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmorModifierBase : MonoBehaviour, IDamageModifier
{
    public int Priority => 100;
    public abstract float ModifyDamage(float incomingDamage);
}
