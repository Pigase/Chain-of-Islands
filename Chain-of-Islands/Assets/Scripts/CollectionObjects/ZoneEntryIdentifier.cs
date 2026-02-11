using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneEntryIdentifier : MonoBehaviour
{
    public event Action<List<StackDiscardedItems>> OnCollected;
    public event Action<bool> IsZoneEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CollectibleObject>())
        {
            IsZoneEntered?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CollectibleObject>())
        {
            IsZoneEntered?.Invoke(false);
            Debug.Log("ExitZone");
        }
    }
}
