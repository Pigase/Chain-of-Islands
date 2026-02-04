using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UIInventorySlot _slotPrefab;
    [SerializeField] private UIInventorySlot _hotSlotPrefab;
    [SerializeField] private UIInventorySlot _armorSlotPrefab;
    [SerializeField] private RectTransform InventaryContentPanel;
    [SerializeField] private RectTransform HotInventaryContentPanel;
    [SerializeField] private RectTransform ArmorInventaryContentPanel;
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventarySystem inventorySystem;

    public List<UIInventorySlot> inventorySlots => slots;

    private ItemDataBase itemDataBase;

    private List<UIInventorySlot> slots;

    public event Action<int, int> OnItemsSwapped;
    public event Action<UIInventorySlot> OnSlotClicked;

    private void Start()
    {
        itemDataBase = GameManager.GetSystem<ItemDataBase>();

        slots = new List<UIInventorySlot>();
        CreateAllSlots(inventory.inventarySlotsCount, inventory.hotInventarySlotsCount, inventory.armorSlotsCount);
        inventorySystem.IInventaryChange += RefreshAllSlots;
        RefreshAllSlots();
    }

    public void RefreshAllSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i]?.RefreshSlotUI(inventory.Slots[i]);
        }
    }

    private void CreateAllSlots(int slotsCount, int hotSlotsCount, int ArmorSlotCount)
    {
        CreateHotInventarySlots(hotSlotsCount);
        CreateInventarySlots(slotsCount);
        CreateSlotforArmor(ArmorSlotCount);
    }

    private void HandleItemClick(UIInventorySlot ClickedSlot)
    {
        OnSlotClicked?.Invoke(ClickedSlot);
    }

    private void HandleItemDrop(UIInventorySlot droppedSlot, UIInventorySlot draggedSlot)
    {
        var droppedSlotIndex = slots.IndexOf(droppedSlot);
        var draggedSlotIndex = slots.IndexOf(draggedSlot);

        if (inventory.Slots[draggedSlotIndex].empty)
            return;

        var itemInfo = itemDataBase.GetItem(inventory.Slots[draggedSlotIndex].itemId);
        
        if(draggedSlot.type == UIInventorySlot.SlotType.ArmorSlot || droppedSlot.type == UIInventorySlot.SlotType.ArmorSlot)
        {
            if (itemInfo != null && itemInfo.ItemType == ItemType.Armor)
            {
                ArmorItem item = (ArmorItem)itemInfo;
                if (item.armorType == droppedSlot.armorType)
                {
                    OnItemsSwapped?.Invoke(droppedSlotIndex, draggedSlotIndex);
                    return;
                }
                if (!inventory.Slots[droppedSlotIndex].empty)
                {
                    return;
                }

            }
        }

        if (droppedSlot.type != UIInventorySlot.SlotType.ArmorSlot)
            OnItemsSwapped?.Invoke(droppedSlotIndex, draggedSlotIndex);

    }

    private void CreateHotInventarySlots(int slotsCount)
    {
        for (int i = 0; i < slotsCount; i++)
        {
            var slot = Instantiate(_hotSlotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(HotInventaryContentPanel);
            slot.type = UIInventorySlot.SlotType.HotInventarySlot;
            slot.OnItemDropped += HandleItemDrop;
            slot.OnSlotSelected += HandleItemClick;
            slot.GetComponent<DragSystem>().OnClickedOnSlot += HandleItemClick;
            slots.Add(slot);
        }
    }

    private void CreateInventarySlots(int slotsCount)
    {
        for (int i = 0; i < slotsCount; i++)
        {
            var slot = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(InventaryContentPanel);
            slot.type = UIInventorySlot.SlotType.InventarySlot;
            slot.OnItemDropped += HandleItemDrop;
            slot.OnSlotSelected += HandleItemClick;
            slot.GetComponent<DragSystem>().OnClickedOnSlot += HandleItemClick;
            slots.Add(slot);
        }
    }

    private void CreateSlotforArmor(int slotsCount)
    {
        ArmorItem.ArmorType[] armorSlotsToCreate =
        {
            ArmorItem.ArmorType.Helmet,
            ArmorItem.ArmorType.Breastplate,
            ArmorItem.ArmorType.Leggings
        };

        int slotsToCreate = Mathf.Min(slotsCount, armorSlotsToCreate.Length);

        for (int i = 0; i < slotsToCreate; i++)
        {
            var slot = Instantiate(_armorSlotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(ArmorInventaryContentPanel);
            slot.type = UIInventorySlot.SlotType.ArmorSlot;
            slot.armorType = armorSlotsToCreate[i];
            slot.OnItemDropped += HandleItemDrop;
            slot.OnSlotSelected += HandleItemClick;
            slot.GetComponent<DragSystem>().OnClickedOnSlot += HandleItemClick;
            slots.Add(slot);
        }
    }

    private void OnDisable()
    {
        inventorySystem.IInventaryChange -= RefreshAllSlots;
    }
}
