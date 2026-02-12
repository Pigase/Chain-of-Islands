using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory
{
    private List<InventorySlot> slots = new List<InventorySlot>();
    public List<InventorySlot> Slots => slots;

    public int maxSlots { get; private set; }

    public int hotInventarySlotsCount = 5;
    public int inventarySlotsCount = 10;
    public int armorSlotsCount { get; private set; } = 3;

    public Inventory()
    {
        Debug.Log("Create Inventory slot");

        maxSlots = hotInventarySlotsCount + inventarySlotsCount + armorSlotsCount;

        for (int i = 0; i < maxSlots; i++)
        {
            InventorySlot newSlot = new InventorySlot();
            slots.Add(newSlot);
        }
    }
}
