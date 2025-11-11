using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UIInventary : MonoBehaviour
{
    [SerializeField] private UIInventarySlot _slotPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventarySystem inventorySystem;

    private List<UIInventarySlot> slots ;
    public event Action<int,int> KnowSwapItemIndex;

    private void Start()
    {
        slots = new List<UIInventarySlot>();
        CreateSlots(inventory.maxSlots);
        RefreshAllSlots();
    }

    private void CreateSlots(int slotsCount)
    {
        for (int i = 0; i < slotsCount; i++)
        {
            var slot = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(contentPanel);
            slots.Add(slot);
            
            inventorySystem.IInventaryChange += RefreshAllSlots;
            slots[i].IItemDrop += ShowIndex;
            slots[i].ISwap += RefreshAllSlots;
            slots[i].IItemSwap += InventaryChange;
        }
    }
    
    public void RefreshAllSlots()
    {
        for(int i = 0;i < slots.Count;i++)
        {
            slots[i].RefreshSlotUI(inventory.slots[i]);
        }
    }

    private int ShowIndex(UIInventarySlot item)
    {
        return slots.IndexOf(item);
    }
    private void InventaryChange(int OneIndex, int TwoIndex)
    {
        KnowSwapItemIndex?.Invoke(OneIndex,TwoIndex);
    }
}
