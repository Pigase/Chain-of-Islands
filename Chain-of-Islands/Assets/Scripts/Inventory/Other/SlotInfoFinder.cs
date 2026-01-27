using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class SlotInfoFinder : MonoBehaviour
{
    [SerializeField] private UIInventory _uIInventory;
    [SerializeField] private Inventory _inventory;

    private ItemDataBase itemData;

    private void Start()
    {
        itemData = GameManager.GetSystem<ItemDataBase>();
    }

    public int SlotIndexFind(UIInventorySlot slot)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot), "Slot cannot be null");
        }

        var slotIndex = _uIInventory.inventorySlots.IndexOf(slot);
        
        return slotIndex;
    }

    public int ItemCountInSlot(UIInventorySlot slot)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot), "Slot cannot be null");
        }

        return _inventory.Slots[SlotIndexFind(slot)].itemCount;
    }

    public string ItemIdInSlot(UIInventorySlot slot)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot), "Slot cannot be null");
        }

        return _inventory.Slots[SlotIndexFind(slot)].itemId;
    }

    public bool IsHotbarSlot(UIInventorySlot slot)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot), "Slot cannot be null");
        }

        if (slot.type == UIInventorySlot.SlotType.HotInventarySlot)
            return true;

        return false;
    }

    public InventorySlot InventorySlotInUISlot(UIInventorySlot slot)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot), "Slot cannot be null");
        }
        return _inventory.Slots[SlotIndexFind((slot))];
    }

    public Item ItemInSlot(UIInventorySlot slot)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot), "Slot cannot be null");
        }
        var item = itemData.GetItem(ItemIdInSlot(slot));
        return item;
    }
    

    public bool IsEmptySlot(UIInventorySlot slot)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot), "Slot cannot be null");
        }

        return _inventory.Slots[SlotIndexFind(slot)].empty;
    }

    public UIInventorySlot SlotByIndex(int index)
    {
        return _uIInventory.inventorySlots[index]; 
    }
}
