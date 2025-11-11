using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMobs : MonoBehaviour
{
    [SerializeField] private Mob _prafabMobToSpawn;
    [SerializeField] private int _poolCount = 4;
    [SerializeField] private bool _autoExpande = true;

    [SerializeField] private SpawnMobToPoint[] _pointsToSpawnMobs;

    private PoolMono<Mob> _poolPrefabMobToSpawn;

    private void Awake()
    {
        _poolPrefabMobToSpawn = new PoolMono<Mob>(_prafabMobToSpawn, _poolCount);
        _poolPrefabMobToSpawn.autoExpand = _autoExpande;

        ReSpawn();
    }

    private void ReSpawn()
    {
        for (int i = 0; i < _pointsToSpawnMobs.Length; i++)
        {
            if(!_pointsToSpawnMobs[i].IsSpawning)
            {
                _pointsToSpawnMobs[i].Spawn(_poolPrefabMobToSpawn);
            }
        }
    }
}
