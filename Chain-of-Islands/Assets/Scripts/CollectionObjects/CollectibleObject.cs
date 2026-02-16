using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;

    public List<StackDiscardedItems> _discardedItems;

    private void OnEnable()
    {
        healthComponent.OnDeath += Collect;
    }
    private void OnDisable()
    {
        healthComponent.OnDeath -= Collect;
    }

    public void Collect()
    {
        healthComponent.TakeDamage(100);
        gameObject.SetActive(false);
    }
}
