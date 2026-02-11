using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public List<StackDiscardedItems> collectoredItems;

    public event Action<List<StackDiscardedItems>> OnCollectoredItems;

    private InventarySystem _inventarySystem;

    private void OnEnable()
    {
        _inventarySystem = GameManager.GetSystem<InventarySystem>();

        for(int i = 0; i < collectoredItems.Count; i++)
        {
            _inventarySystem.AddItems(collectoredItems[i].Item.itemId, collectoredItems[i].amount);
            Debug.Log("Collector");
        }
        gameObject.SetActive(false);
    }
}
