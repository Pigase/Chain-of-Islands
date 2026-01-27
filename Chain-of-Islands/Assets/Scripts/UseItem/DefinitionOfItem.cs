using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitionOfItem : MonoBehaviour
{
    [SerializeField] private ClickHandler clickHandler;

    private ItemDataBase itemData;

    public event Action<Item> OnItemDefined;

    private void Start()
    {
        itemData = GameManager.GetSystem<ItemDataBase>();
    }

    private void ItemDefinition(InventorySlot slot)
    {
        var item = slot.itemId;
        var itemInfo = itemData.GetItem(item);

        if(itemInfo != null)
            OnItemDefined?.Invoke(itemInfo);
    }

    private void OnEnable()
    {
        clickHandler.OnClicedOnHotSlot += ItemDefinition;
    }

    private void OnDisable()
    {
        clickHandler.OnClicedOnHotSlot -= ItemDefinition;
    }
}
