using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory
{
    public Dictionary<string, int> items = new Dictionary<string, int>();
    public int maxSlots = 20;


    public void AddItem(string itemId, int amount)
    {
        ItemDataBase itemDataBase = GameManager.GetSystem<ItemDataBase>();

        var itemData = itemDataBase.GetItem(itemId);

        if(items.Count < maxSlots)
        {
            if (!items.ContainsKey(itemId))
            {
                items.Add(itemId, amount);
            }
        }
        else
        {
            Debug.Log("Инвентарь переполнен ");
        }
    }
    private void PutItem(string itemId, int amount)
    {
        ItemDataBase itemDataBase = GameManager.GetSystem<ItemDataBase>();

        var itemData = itemDataBase.GetItem(itemId);

        int result = items[itemId] + amount;

        if (result > itemData.maxStackSize)
        {
            items[itemId] = itemData.maxStackSize;
            
            var remainder = result - itemData.maxStackSize;

            AddItem(itemId, remainder);
        }
        else 
        {
            items[itemId] = result;
        }
    }
    public void RemoveItem(string itemId) 
    {
        if(items.ContainsKey(itemId))
        {
            items.Remove(itemId);
        }
    }

    public void SubtractItem(string itemId, int amount)
    {
        if(items.ContainsKey(itemId))
        {
            var result = items[itemId] - amount;

            if(result <= 0)
            {
                items[itemId] = 0;
                RemoveItem(itemId);
            }
        }
    }
}
