using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private MobAI _mobAI;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private HealthComponent _health;

    private void OnEnable()
    {
        _sprite.color = Color.white;
        StopAllCoroutines();
        _health.OnDeath += MobDie;
        _health.OnDamageTaken += MobTakeDamage;
    }

    private void OnDisable()
    {
        _health.OnDeath -= MobDie;
        _health.OnDamageTaken -= MobTakeDamage;
    }

    private void Update()
    {
        if (_mobAI != null)
        {
            _mobAI.ChooseState();
        }
    }

    private void MobDie()
    {
        gameObject.SetActive(false);
    }

    private void MobTakeDamage(float damage, float timeInvulnerability)
    {
        StartCoroutine(Invulnerability(timeInvulnerability));
    }

    private IEnumerator Invulnerability(float secondsForShowDamge)
    {

        _sprite.color = Color.red;
        yield return new WaitForSeconds(secondsForShowDamge);
        _sprite.color = Color.white;
    }
}