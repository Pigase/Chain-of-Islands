using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory: MonoBehaviour
{
    private List<InventorySlot> slots = new List<InventorySlot>();
    public List<InventorySlot> Slots => slots;

    public int maxSlots { get; private set; }

    public int hotInventarySlotsCount = 5;
    public int inventarySlotsCount = 10;
    public int armorSlotsCount { get; private set; } = 3;

    private void Awake()
    {
        maxSlots = hotInventarySlotsCount + inventarySlotsCount + armorSlotsCount;

        for(int i = 0; i < maxSlots; i++)
        {
            InventorySlot newSlot = new InventorySlot();

            slots.Add(newSlot);
        }
    }
}
