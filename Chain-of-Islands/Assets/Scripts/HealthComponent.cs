using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth;
    private bool _isAlive = true;

    // События для связи с другими системами
    public event Action<float> OnDamageTaken; // float - полученный урон
    public event Action OnDeath;
    public event Action<float> OnHealed;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsAlive => _isAlive;

    private void Awake() => ResetHealth();

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        _isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        if (!_isAlive) return;

        _currentHealth -= damage;
        OnDamageTaken?.Invoke(damage);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isAlive = false;
            OnDeath?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        OnHealed?.Invoke(amount);
    }
}