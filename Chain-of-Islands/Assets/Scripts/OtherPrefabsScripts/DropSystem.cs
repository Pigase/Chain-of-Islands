using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class DropSystem : MonoBehaviour
{
    [SerializeField] private Object _object;
    [SerializeField] private Transform _itemDroppingPosition;
    [SerializeField] private HealthComponent healthComponent;

    private ItemDataBase itemDate;
    private WorldPool _pool;
    private List<ItemStack> itemDrop;

    private void Start()
    {
        itemDate = GameManager.GetSystem<ItemDataBase>();
        _pool = GameManager.GetSystem<WorldPool>();

        if(_object == null)
            throw new ArgumentNullException(nameof(_object), "_object cannot be null");

        itemDrop = _object.DispensingItems;
    }

    private void Drop()
    {
        for (int i = 0; i < itemDrop.Count; i++)
        {
            Item item = itemDate.GetItem(itemDrop[i].itemId);
            if(item != null)
            {
                DroppingItems(item, itemDrop[i].amount);
            }
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
    }

    private void OnEnable()
    {
        healthComponent.OnDeath += Drop;
    }

    private void OnDisable()
    {
        healthComponent.OnDeath -= Drop;
    }
}
