using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldItemDrop : MonoBehaviour
{
    [SerializeField] private Object _object;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private float radiusDrop;
    [SerializeField] private Transform itemDroppingPosition;


    private SpawnItemWorldPrefab _spawnItemWorldPrefab;
    public event Func<List<StackDiscardedItems>> OnRequestedForInformation;

    private void Start()
    {
        _spawnItemWorldPrefab = GameManager.GetSystem<SpawnItemWorldPrefab>();
    }

    private void SetItems()
    {
        var items = OnRequestedForInformation?.Invoke();
        for(int i = 0; i < items.Count; i++)
        {
            DropItem(items[i].Item, items[i].amount, itemDroppingPosition, radiusDrop);
        }
    }

    private void DropItem(Item item, int amount, Transform itemDroppingPosition, float radiusDrop)
    {
        _spawnItemWorldPrefab.SpawnItem(item,amount,itemDroppingPosition,radiusDrop);
    }

    private void OnEnable()
    {
        _healthComponent.OnDeath += SetItems;
    }

    private void OnDisable()
    {
        _healthComponent.OnDeath += SetItems;
    }
}
