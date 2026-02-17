using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public List<StackDiscardedItems> _discardedItems;

    public event Action OnCollected;

    public void Collect()
    {
        OnCollected?.Invoke();
        gameObject.SetActive(false);
    }
}
