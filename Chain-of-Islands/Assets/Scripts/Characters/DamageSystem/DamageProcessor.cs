using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageProcessor : MonoBehaviour
{
    [SerializeField]private List<IDamageModifier> _damageModifiers = new List<IDamageModifier>();

    private void Awake()
    {
        // Собираем все IDamageModifier на этом объекте
        _damageModifiers.AddRange(GetComponents<IDamageModifier>());

        // Сортируем по Priority
        _damageModifiers.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }

    public float ProcessDamage(float incomingDamage)
    {
        float finalDamage = incomingDamage;

        // Применяем все модификаторы
        foreach (var modifier in _damageModifiers)
        {
            finalDamage = modifier.ModifyDamage(finalDamage);
        }

        Debug.Log("modif");

        return Mathf.Max(0, finalDamage); // Урон не может быть отрицательным
    }
}