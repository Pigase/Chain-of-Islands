using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobToPoint : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 5f;

    private PoolMono<Mob> _poolPrefabMobToSpawn;

    private bool _isSpawned = false;
    public bool IsSpawning => _isSpawned;
    public void Spawn(PoolMono<Mob> poolPrefabMobToSpawn)
    {
        StartCoroutine(SpawnCoroutine(poolPrefabMobToSpawn));
    }

    private IEnumerator SpawnCoroutine(PoolMono<Mob> poolPrefabMobToSpawn)
    {
        _isSpawned = true;
        yield return new WaitForSeconds(_spawnTime); // ∆дем

        Mob mob = poolPrefabMobToSpawn.GetFreeElement(transform.position);
    }
}
