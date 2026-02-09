using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEditor.Progress;

public class InventarySystem : MonoBehaviour
{
    [SerializeField] private TestInventary testInventary;
    [SerializeField] private UIInventory uiInventary;
    [SerializeField] private Inventory inventory;
    [SerializeField] private DroppingZone _droppingZone;
    [SerializeField] private DestroyZone _destroyZone;
    [SerializeField] private CraftingSystem _craftingSystem;

    public event Action IInventaryChange;

    private int fillableSlots;

    private void Start()
    {
        fillableSlots = inventory.maxSlots - inventory.armorSlotsCount;
    }

    public void AddItems(string itemId, int count)
    {
        if (itemId != null)
        {
            ItemDataBase itemData = GameManager.GetSystem<ItemDataBase>();
            var itemInfo = itemData.GetItem(itemId);

            if (itemInfo == null) return;

            foreach (var item in inventory.Slots)
            {
                if (item.itemId == itemId && item.itemCount < itemInfo.maxStackSize)
                {
                    int freeSpace = itemInfo.maxStackSize - item.itemCount;

                    if (freeSpace <= count)
                    {
                        item.itemCount = itemInfo.maxStackSize;
                        count -= freeSpace;
                    }
                    else
                    {
                        item.itemCount += count;
                        count = 0;
                    }
                }
            }

            for (int i = 0, ostatok = count; i < fillableSlots && ostatok > 0; i++)
            {
                if (inventory.Slots[i].empty)
                {
                    int addAmount = Mathf.Min(ostatok, itemInfo.maxStackSize);

                    inventory.Slots[i].itemId = itemId;
                    inventory.Slots[i].itemCount = addAmount;
                    inventory.Slots[i].empty = false;

                    ostatok -= addAmount;
                }
            }
            IInventaryChange?.Invoke();
        }
    }

    public void SubtractItems(string itemId, int count)
    {
        if (itemId != null)
        {
            for (int i = 0; i < inventory.Slots.Count && count > 0; i++)
            {
                var slot = inventory.Slots[i];

                if (slot.itemId == itemId)
                {
                    if (slot.itemCount <= count)
                    {
                        count -= slot.itemCount;
                        inventory.Slots[i].itemId = null;
                        inventory.Slots[i].empty = true;
                        inventory.Slots[i].itemCount = 0;
                    }
                    else
                    {
                        slot.itemCount -= count;
                        count = 0;
                    }
                }
            }
            IInventaryChange?.Invoke();
        }
    }

    public void SubstractItemFromSlot(InventorySlot slot, int amountItem = 1)
    {
        int index = inventory.Slots.IndexOf(slot);

        inventory.Slots[index].itemCount -=  amountItem;

        if (inventory.Slots[index].itemCount <= 0)
        {
            RemoveItems(index);
        }
    }

    public void RemoveItems(int itemIndex)
    {
        if(itemIndex >= 0)
        {
            inventory.Slots[itemIndex].itemId = null;
            inventory.Slots[itemIndex].empty = true;
            inventory.Slots[itemIndex].itemCount = 0;
        }
        IInventaryChange?.Invoke();
    }

    public void ItemSwap(int indexOneSlot, int indexTwoSlot)
    {
        
        var item = inventory.Slots[indexOneSlot];
        inventory.Slots[indexOneSlot] = inventory.Slots[indexTwoSlot];
        inventory.Slots[indexTwoSlot] = item;
        IInventaryChange?.Invoke();
    }

    public void DebugSlotInfo()
    {
        for (int j = 0; j < inventory.Slots.Count; j++)
        {
            Debug.Log($"Ячейка  {j} пустой ли: {inventory.Slots[j].empty} . Ячейка  {j} количество: {inventory.Slots[j].itemCount}");
        }
    }

    private void OnEnable()
    {
        testInventary.OnItemAdded += AddItems;
        testInventary.OnItemRemove += SubtractItems;
        testInventary.OnItemTest += DebugSlotInfo;
        uiInventary.OnItemsSwapped += ItemSwap;
        _droppingZone.OnDroppedItemIndex += RemoveItems;
        _destroyZone.OnItemDroppedInDestroyZone += RemoveItems;
        _craftingSystem.OnCraftedItemSubtract += SubtractItems;
        _craftingSystem.OnCraftedItemAdded += AddItems;
    }

    private void OnDisable()
    {
        testInventary.OnItemAdded -= AddItems;
        testInventary.OnItemRemove -= SubtractItems;
        testInventary.OnItemTest -= DebugSlotInfo;
        uiInventary.OnItemsSwapped -= ItemSwap;
        _droppingZone.OnDroppedItemIndex -= RemoveItems;
        _destroyZone.OnItemDroppedInDestroyZone -= RemoveItems;
        _craftingSystem.OnCraftedItemSubtract -= SubtractItems;
        _craftingSystem.OnCraftedItemAdded -= AddItems;
    }
}