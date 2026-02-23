using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class WorldItemDrop : MonoBehaviour
{
    [Header("Выпадающие предметы")]
    [Tooltip("Предмет и его количество ")]
    [SerializeField] private List<StackDiscardedItems> _dispensingItems = new List<StackDiscardedItems>();

    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private float _radiusDrop;

    private Vector2 _itemDroppingPosition;

    public Action ResourcesAreSpawned;


    private SpawnItemWorldPrefab _spawnItemWorldPrefab;

    private void Start()
    {
        _spawnItemWorldPrefab = GameManager.GetSystem<SpawnItemWorldPrefab>();

        _itemDroppingPosition = transform.position;
    }

    private void OnEnable()
    {
        _healthComponent.OnDeath += SetItems;
    }

    private void OnDisable()
    {
        _healthComponent.OnDeath -= SetItems;
    }

    private void SetItems()
    {
        var items = _dispensingItems;
        for(int i = 0; i < items.Count; i++)
        {
            DropItem(items[i].Item, items[i].amount, _itemDroppingPosition);
        }

        ResourcesAreSpawned?.Invoke();
    }

    private void DropItem(Item item, int amount, Vector2 itemDroppingPosition)
    {
        _spawnItemWorldPrefab.SpawnItem(item,itemDroppingPosition);
    }


}
