using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [Header("Выпадающие предметы")]
    [Tooltip("ID предмета и его количество ")]
    [SerializeField] private List<ItemStack> dispensingItems = new List<ItemStack>();

    private (Item item, int count)[] _items;
    private ItemDataBase itemDate;

    private void Start()
    {
        ItemsInitialization();
    }

    private void ItemsInitialization()
    {
        itemDate = GameManager.GetSystem<ItemDataBase>();

        _items = new (Item item, int count)[dispensingItems.Count];
        for (int i = 0; i < dispensingItems.Count; i++)
        {
            var itemInfo = itemDate.GetItem(dispensingItems[i].itemId);

            if (itemInfo == null)
                throw new ArgumentNullException(nameof(itemInfo), "Item ID does not exist ");

            _items[i] = (itemInfo, dispensingItems[i].amount);
        }
    }
}
