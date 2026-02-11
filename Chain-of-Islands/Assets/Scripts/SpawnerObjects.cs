using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjects : MonoBehaviour
{
    [SerializeField] private PoolObject _prefabObjectToSpawn;
    [SerializeField] private float _respawnTime = 30f;
    [SerializeField] private int _poolCount = 5;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private SpawnObjectToPoint[] _spawnPoints;

    private PoolMono<PoolObject> _pool;

    private void Awake()
    {
        _pool = new PoolMono<PoolObject> (_prefabObjectToSpawn, _poolCount, transform);
        _pool.autoExpand = _autoExpand;

        foreach (var point in _spawnPoints)
        {
            point.Initialize(_pool, _respawnTime);
        }
    }
}