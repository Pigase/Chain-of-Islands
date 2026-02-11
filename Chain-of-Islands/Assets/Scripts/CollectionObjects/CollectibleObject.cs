using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    [SerializeField] private List<StackDiscardedItems> _discardedItems;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Collector>())
        {
            collision.GetComponent<Collector>().collectoredItems = _discardedItems;
            Debug.Log("Object False");
            gameObject.SetActive(false);

        }
    }
}
