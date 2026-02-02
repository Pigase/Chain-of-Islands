using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class DropSystem : MonoBehaviour
{
    [SerializeField] private Transform _itemDroppingPosition;
    [SerializeField] private HealthComponent healthComponent;

    private WorldPool _pool;
    private List<StackDiscardedItems> itemDrop;

    public event Func<List<StackDiscardedItems>> OnRequestedForInformation;

    private void Start()
    {
        _pool = GameManager.GetSystem<WorldPool>();
    }

    private void RequestForInformation()
    {
        itemDrop = OnRequestedForInformation?.Invoke();

        Drop();
    }
    private void Drop()
    {
        for (int i = 0; i < itemDrop.Count; i++)
        {
            DroppingItems(itemDrop[i].Item, itemDrop[i].amount);
        }
    }

    private void DroppingItems(Item item, int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            var dropItem = _pool.GetFreeElement();
            dropItem.SpriteRenderer.sprite = item.worldPrefabIcon;
            DropPosition(_itemDroppingPosition.transform, dropItem,2);
        }
    }

    private void DropPosition(Transform itemDroppingPosition, ItemPrefab item, int radiusDrop)
    {
        item.transform.position = itemDroppingPosition.position;

        Vector2 randomOffset = Random.insideUnitCircle * radiusDrop;
        Vector3 dropPos = itemDroppingPosition.position + new Vector3(randomOffset.x, 1f, randomOffset.y);

        item.transform.position = dropPos;
    }

    private void OnEnable()
    {
        healthComponent.OnDeath += RequestForInformation;
    }

    private void OnDisable()
    {
        healthComponent.OnDeath -= RequestForInformation;
    }
}
