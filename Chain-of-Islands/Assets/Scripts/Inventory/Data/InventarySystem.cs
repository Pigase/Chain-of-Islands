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
    [SerializeField] private DroppingZone _droppingZone;
    [SerializeField] private DestroyZone _destroyZone;
    [SerializeField] private CraftingSystem _craftingSystem;

    public Inventory Inventory => inventory;

    private Inventory inventory;
    private ItemDataBase itemData;
    private int fillableSlots;
    private List<int> armorSlotsIndex;

    public event Action IInventaryChange;

    public void Initialize()
    {
        inventory = new Inventory();
        itemData = GameManager.GetSystem<ItemDataBase>();
        armorSlotsIndex = new List<int>();
        fillableSlots = inventory.maxSlots - inventory.armorSlotsCount;
        armorSlotsIndex = uiInventary.ArmorSlotsIndexs;

    }

    public void AddItems(string itemId, int count)
    {
        if (itemId != null)
        {
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

    public List<InventorySlot> GetArmorSlots()
    {

        armorSlotsIndex = uiInventary.ArmorSlotsIndexs;

        List<InventorySlot> armorSlots = new List<InventorySlot>();

        for (int i = 0; i < armorSlotsIndex.Count; i++)
        {
            armorSlots.Add(inventory.Slots[armorSlotsIndex[i]]);
        }
        return armorSlots;
    }

    public bool IsCanAddItem(string itemId)
    {
        Item iteminfo = itemData.GetItem(itemId);

        bool can = false;

        for(int i = 0; i < fillableSlots; i ++)
        {
            if (inventory.Slots[i].empty)
            {
                can = true;
                return can;
            }
            if (!inventory.Slots[i].empty && inventory.Slots[i].itemId == itemId && inventory.Slots[i].itemCount < iteminfo.maxStackSize)
            {
                can = true;
                return can;
            }
        }
        return false;
    }

    public void SubtractItems(string itemId, int count)
    {
        if (itemId != null)
        {
            for (int i = 0; i < Inventory.Slots.Count && count > 0; i++)
            {
                var slot = Inventory.Slots[i];

                if (slot.itemId == itemId)
                {
                    if (slot.itemCount <= count)
                    {
                        count -= slot.itemCount;
                        Inventory.Slots[i].itemId = null;
                        Inventory.Slots[i].empty = true;
                        Inventory.Slots[i].itemCount = 0;
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
        int index = Inventory.Slots.IndexOf(slot);

        Inventory.Slots[index].itemCount -=  amountItem;

        if (Inventory.Slots[index].itemCount <= 0)
        {
            RemoveItems(index);
        }
        IInventaryChange?.Invoke();
    }

    public void RemoveItems(int slotIndex)
    {
        if(slotIndex >= 0)
        {
            Inventory.Slots[slotIndex].itemId = null;
            Inventory.Slots[slotIndex].empty = true;
            Inventory.Slots[slotIndex].itemCount = 0;
        }
        IInventaryChange?.Invoke();
    }

    public void ItemSwap(int indexOneSlot, int indexTwoSlot)
    {
        
        var item = Inventory.Slots[indexOneSlot];
        Inventory.Slots[indexOneSlot] = Inventory.Slots[indexTwoSlot];
        Inventory.Slots[indexTwoSlot] = item;
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