using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionJoystick : MonoBehaviour
{
    [SerializeField] private ClickHandler _hotInventory;

    private InventorySlot _currentItem;

    public Action<InventorySlot> ButtonPressed;

    private void OnEnable()
    {
        _hotInventory.OnClicedOnHotSlot += SaveCurrentItemInHotInventary;
        _hotInventory.OnClicedOnSlot += SaveCurrentItemInHotInventary;
    }

    private void OnDisable()
    {
        _hotInventory.OnClicedOnHotSlot -= SaveCurrentItemInHotInventary;
        _hotInventory.OnClicedOnSlot -= SaveCurrentItemInHotInventary;
    }

    public void UseItem()
    {
        ButtonPressed?.Invoke(_currentItem);
    }

    private void SaveCurrentItemInHotInventary (InventorySlot currentItem)
    {
        _currentItem = currentItem;
    }
}
