using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _timeInvulnerability;
    [SerializeField] private Slider _healthBar;
    
    private bool _invulnerability = false;
    private bool _healingRecharge = false;
    private bool _isAlive = true;

    // События для связи с другими системами
    public event Action<float,float> OnDamageTaken; // полученный урон, время неуязвимости
    public event Action OnDeath;
    public event Action<float> OnSubjectHealed;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsAlive => _isAlive;

    private void Awake() => ResetHealth();

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        _invulnerability = false;
        _healingRecharge = false;
        StopAllCoroutines();
        _isAlive = true;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (!_isAlive || _invulnerability) return;

        _invulnerability = true; // СРАЗУ устанавливаем флаг

        _currentHealth -= damage;
        OnDamageTaken?.Invoke(damage, _timeInvulnerability);
        UpdateHealthBar();  

        StartCoroutine(InvulnerabilityRecharge());

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isAlive = false;
            OnDeath?.Invoke();
        }
    }

    public void HealFromSubject(float amount, float timeToHealingRecharge = 0)
    {
        if (!_isAlive || _healingRecharge)
            return;

        _healingRecharge = true;

        Debug.Log(amount);
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        OnSubjectHealed?.Invoke(amount);
        UpdateHealthBar();

        StartCoroutine(HealingRecharge(timeToHealingRecharge));
    }

    private void UpdateHealthBar()
    {
        _healthBar.value = _currentHealth;
    }

    private IEnumerator InvulnerabilityRecharge()
    {
        yield return new WaitForSeconds(_timeInvulnerability);
        _invulnerability = false; // Сбрасываем через время
    }

    private IEnumerator HealingRecharge(float timeToHealingRecharge)
    {
        yield return new WaitForSeconds(timeToHealingRecharge);
        _healingRecharge = false; // Сбрасываем через время
    }
}