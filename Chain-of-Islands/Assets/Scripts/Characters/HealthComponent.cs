using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _timeInvulnerability;
    
    private bool _invulnerability = false;
    private bool _isAlive = true;

    // События для связи с другими системами
    public event Action<float,float> OnDamageTaken; // полученный урон, время неуязвимости
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
        if (!_isAlive || _invulnerability) return;

        _invulnerability = true; // СРАЗУ устанавливаем флаг

        _currentHealth -= damage;
        OnDamageTaken?.Invoke(damage, _timeInvulnerability);

        StartCoroutine(InvulnerabilityCooldown());

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isAlive = false;
            OnDeath?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        Debug.Log(amount);
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        OnHealed?.Invoke(amount);
    }

    private IEnumerator InvulnerabilityCooldown()
    {
        yield return new WaitForSeconds(_timeInvulnerability);
        _invulnerability = false; // Сбрасываем через время
    }
}