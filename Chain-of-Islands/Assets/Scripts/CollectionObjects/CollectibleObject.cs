using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public List<StackDiscardedItems> _discardedItems;

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}
