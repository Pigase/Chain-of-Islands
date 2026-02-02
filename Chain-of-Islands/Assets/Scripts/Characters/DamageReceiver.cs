using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;    
    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }
}
