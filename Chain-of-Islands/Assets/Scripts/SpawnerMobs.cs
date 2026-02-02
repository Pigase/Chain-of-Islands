using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMobs : MonoBehaviour
{
    [SerializeField] private Mob _prafabMobToSpawn;
    [SerializeField] private float _respawnTime = 30f;
    [SerializeField] private int _poolCount = 5;
    [SerializeField] private bool _autoExpande = true;

    [SerializeField] private SpawnMobToPoint[] _pointsToSpawnMobs;

    private PoolMono<Mob> _poolPrefabMobToSpawn;

    private void Awake()
    {
        _poolPrefabMobToSpawn = new PoolMono<Mob>(_prafabMobToSpawn, _poolCount, transform);
        _poolPrefabMobToSpawn.autoExpand = _autoExpande;

        foreach (var point in _pointsToSpawnMobs)
        {
            point.Initialize(_poolPrefabMobToSpawn, _respawnTime);
        }

    }
}