using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SelectionItem : MonoBehaviour
{
    private InventarySystem inventarySystem;

    private void Start()
    {
        inventarySystem = GameManager.GetSystem<InventarySystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (inventarySystem.IsCanAddItem(collision.GetComponent<SelectableItem>()?.item.itemId))
            {
                Selection(collision.GetComponent<SelectableItem>().item);
                collision?.GetComponent<SelectableItem>()?.SelectItem();
            }
        }
    }

    private void Selection(Item item)
    {
        inventarySystem.AddItems(item.itemId, 1);
    }
}
