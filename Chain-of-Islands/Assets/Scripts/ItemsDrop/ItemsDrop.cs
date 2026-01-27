using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsDrop : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpande;
    [SerializeField] private ItemPrefab _prefab;
    [SerializeField] private DroppingZone droppingZone;

    private ItemDataBase itemDate;
    private PoolMono<ItemPrefab> _pool;

    private void Start()
    {
        itemDate = GameManager.GetSystem<ItemDataBase>();

        _pool = new PoolMono<ItemPrefab>(_prefab, _poolCount, transform);
        _pool.autoExpand = _autoExpande;
    }

    private void DroppingItems(Item item, int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            var dropItem = _pool.GetFreeElement();
            dropItem.SpriteRenderer.sprite = item.worldPrefabIcon;
        }        
    }

    private void OnEnable()
    {
        droppingZone.OnItemDropped += DroppingItems;
    }

    private void OnDisable()
    {
        droppingZone.OnItemDropped -= DroppingItems;
    }
}
