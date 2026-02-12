using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private UIInventory _uiInventory;
    [SerializeField] private SlotInfoFinder _slotInfoFinder;

    private InventarySystem _inventarySystem;
    private int _selectedHotbarIndex = 0;
    private int _lastSelectedIndex = 0;

    public event Action<InventorySlot> OnClicedOnHotSlot;
    public event Action<InventorySlot> OnClicedOnSlot;

    private void OnEnable()
    {
        _uiInventory.OnSlotClicked += HandleSlotClick;
        _uiInventory.OnSlotClicked += ReadCliced;
    }
    private void OnDisable()
    {
        _uiInventory.OnSlotClicked -= HandleSlotClick;
    }

    private void Start()
    {
        _inventarySystem = GameManager.GetSystem<InventarySystem>();
        SelectSlot(_selectedHotbarIndex);
        OnClicedOnHotSlot?.Invoke(_inventarySystem.Inventory.Slots[_selectedHotbarIndex]);
    }

    public void SelectHotSlotRefresh()
    {
        SelectSlot(_selectedHotbarIndex);
        _lastSelectedIndex = _selectedHotbarIndex;
        OnClicedOnHotSlot?.Invoke(_inventarySystem.Inventory.Slots[_selectedHotbarIndex]);
    }

    private void HandleSlotClick(UIInventorySlot clickedSlot)
    {
        if(clickedSlot != null)
        {
            int slotIndex = _uiInventory.inventorySlots.IndexOf(clickedSlot);

            if (_lastSelectedIndex == slotIndex)
                return;

            SelectSlot(slotIndex);

            _lastSelectedIndex = slotIndex;

            if (_slotInfoFinder.IsHotbarSlot(clickedSlot))
            {
                _selectedHotbarIndex = slotIndex;
                OnClicedOnHotSlot?.Invoke(_slotInfoFinder.InventorySlotInUISlot(clickedSlot));
                return;
            }
            OnClicedOnSlot?.Invoke(_slotInfoFinder.InventorySlotInUISlot(clickedSlot));
        }
    }

    private void SelectSlot(int slotIndex)
    {
        for(int i = 0; i < _uiInventory.inventorySlots.Count; i++)
        {
            _uiInventory.inventorySlots[i].SelectSlot(false);
        }
        _uiInventory.inventorySlots[slotIndex].SelectSlot(true);
    }

    private void ReadCliced(UIInventorySlot slot )
    {

    } 
}
