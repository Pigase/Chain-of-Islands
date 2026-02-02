using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobToPoint : MonoBehaviour
{
    private float _respawnTime = 30f;
    private Mob _currentMob;
    private PoolMono<Mob> _pool;
    private HealthComponent _currentMobHealth;

    public void Initialize(PoolMono<Mob> pool, float respawnTime)
    {
        _respawnTime = respawnTime;
        _pool = pool;
        SpawnMob();
    }

    private void SpawnMob()
    {
        _currentMob = _pool.GetFreeElement(transform.position);
        _currentMobHealth = _currentMob.GetComponent<HealthComponent>();
        _currentMobHealth.OnDeath += HandleMobDeath;
    }

    private void HandleMobDeath()
    {
        // Сразу отписываемся
        if (_currentMobHealth != null)
        {
            _currentMobHealth.OnDeath -= HandleMobDeath;
            _currentMobHealth = null;
        }

        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        // Ждем respawnTime
        yield return new WaitForSeconds(_respawnTime);

        SpawnMob(); // Спавним нового моба
    }
}