using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private List<StackDiscardedItems> collectoredItems;

    private InventarySystem _inventarySystem;
    private List<CollectibleObject> collectibleObjects;

    public event Action<List<StackDiscardedItems>> OnCollected;
    public event Action<bool> IsZoneEntered;
    public event Action<List<StackDiscardedItems>> OnCollectoredItems;

    private void Start()
    {
        _inventarySystem = GameManager.GetSystem<InventarySystem>();
        collectibleObjects = new List<CollectibleObject>();
    }

    public void Collectore()
    {
        CollectibleObject currentCollectibleObject = null;

        if(collectibleObjects != null)
        {
            currentCollectibleObject = CalculationCurrentObject(collectibleObjects);

            collectoredItems = currentCollectibleObject._discardedItems;

            for (int i = 0; i < collectoredItems.Count; i++)
            {
                _inventarySystem.AddItems(collectoredItems[i].Item.itemId, collectoredItems[i].amount);
            }
            currentCollectibleObject.Collect();
        }
    }

    private CollectibleObject CalculationCurrentObject(List<CollectibleObject> collectibleObjects)
    {
        if(collectibleObjects.Count > 1)
        {
            float minDistance = Mathf.Infinity;
            float distance;
            CollectibleObject currentObject = null;
            foreach (var item in collectibleObjects)
            {
                distance = PlayerService.FindDistanceToPlayer(item.transform.position);

                if(distance <= minDistance)
                {
                    minDistance = distance;
                    currentObject = item;
                }

            }
            return currentObject;
        }
        return collectibleObjects[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CollectibleObject>())
        {
            collectibleObjects.Add(collision.GetComponent<CollectibleObject>());
            IsZoneEntered?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collectibleObjects.Count);
        if (collision.GetComponent<CollectibleObject>())
        {
            if(collectibleObjects.Count <= 1)
            {
                IsZoneEntered?.Invoke(false);
            }
            collectibleObjects.Remove(collision.GetComponent<CollectibleObject>());
        }
    }
}
