using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectToPoint : MonoBehaviour
{
    private float _respawnTime = 30f;
    private PoolObject _currentObject;
    private PoolMono<PoolObject> _pool;
    private HealthComponent _currentObjectHealth;

    public void Initialize(PoolMono<PoolObject> pool, float respawnTime)
    {
        _respawnTime = respawnTime;
        _pool = pool;
        SpawnObject();
    }

    private void SpawnObject()
    {
        _currentObject = _pool.GetFreeElement(transform.position);
        if (_currentObject == null) return;

        SetupObject(_currentObject);
    }

    private void SetupObject(PoolObject obj)
    {
        _currentObject = obj;
        _currentObject.transform.position = transform.position;
        _currentObjectHealth = _currentObject.GetComponent<HealthComponent>();
        _currentObjectHealth.ResetHealth();


        if (_currentObjectHealth != null)
        {
            _currentObjectHealth.OnDeath -= HandleObjectDeath;
            _currentObjectHealth.OnDeath += HandleObjectDeath;
        }
    }

    private void HandleObjectDeath()
    {
        if (_currentObjectHealth != null)
        {
            _currentObjectHealth.OnDeath -= HandleObjectDeath;
            _currentObjectHealth = null;
        }
        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(_respawnTime);
        SpawnObject();
    }
}