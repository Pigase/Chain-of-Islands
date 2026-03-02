using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class WorldItemDrop : MonoBehaviour
{
    [Header("Выпадающие предметы")]
    [Tooltip("Предмет и его количество ")]
    [SerializeField] private List<StackDiscardedItems> _dispensingItems = new List<StackDiscardedItems>();
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private float _radiusDrop = 1;

    public Action ResourcesAreSpawned;

    private SpawnItemWorldPrefab _spawnItemWorldPrefab;

    private void Start()
    {
        _spawnItemWorldPrefab = GameManager.GetSystem<SpawnItemWorldPrefab>();
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
            for (int j = 0; j < items[i].amount; j++)
            {
                Vector2 itemPos = CalculateDropPosition();
                _spawnItemWorldPrefab.SpawnItem(items[i].Item, itemPos);
            }
        }

        ResourcesAreSpawned?.Invoke();
    }
    private Vector2 CalculateDropPosition()
    {
        float xPos = Random.Range(-_radiusDrop + transform.position.x, _radiusDrop + transform.position.x);
        float yPos = Random.Range(-_radiusDrop + transform.position.y, _radiusDrop + transform.position.y);

        Vector2 itemPos = new Vector2(xPos, yPos);

        return itemPos;
    }
}
