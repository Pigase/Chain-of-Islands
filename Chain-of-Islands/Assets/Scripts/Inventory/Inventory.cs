using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory: MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public int maxSlots = 5;

    private void Awake()
    {
        for(int i = 0; i < maxSlots; i++)
        {
            InventorySlot newSlot = new InventorySlot();

            slots.Add(newSlot);
        }
    }
}
