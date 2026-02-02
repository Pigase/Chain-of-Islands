using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPool : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpande;
    [SerializeField] private ItemPrefab _prefab;

    private PoolMono<ItemPrefab> _pool;

    public void Initialize()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        _pool = new PoolMono<ItemPrefab>(_prefab, _poolCount, transform);
        _pool.autoExpand = _autoExpande;
    }

    public ItemPrefab GetFreeElement()
    {
        return _pool.GetFreeElement();
    }
}
