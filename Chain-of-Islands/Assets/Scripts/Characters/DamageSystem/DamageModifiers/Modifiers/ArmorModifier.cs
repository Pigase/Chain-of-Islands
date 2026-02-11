using UnityEngine;

public class ArmorModifier : MonoBehaviour, IDamageModifier
{
    [Header("Настройки брони")]
    [SerializeField] private float _physicalResistance = 10f;

    private int _priority = 100; // Применяется после щитов, до иммунитетов

    public int Priority => _priority;

    public float ModifyDamage(float incomingDamage)
    {
        float resistance = GetResistance();
        float reducedDamage = incomingDamage - resistance;

        Debug.Log($"Armor reduced damage: {incomingDamage} → {reducedDamage} (-{resistance * 100}%)");

        return reducedDamage;
    }

    private float GetResistance()
    {
       return _physicalResistance;
    }
}