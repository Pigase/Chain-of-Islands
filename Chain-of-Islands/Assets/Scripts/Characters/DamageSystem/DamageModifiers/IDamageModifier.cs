using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageModifier
{
    int Priority { get; } // Меньше число = раньше применяется
    float ModifyDamage(float incomingDamage);
}
