using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseHandler : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;

    private ItemDataBase _itemData;

    private void Start()
    {
        _itemData = GameManager.GetSystem<ItemDataBase>();//надо обьявлять в старте т.к. система только в awake создаеться
    }

    public void UseItem(InventorySlot slot)
    {
        if (slot.empty)
            return;

        Item item = _itemData.GetItem(slot.itemId);

        switch (item)
        {
            case RestorativeHealthItem healthItem:

                UseRestorativeHealthItem(healthItem);

                break;

            case WeaponItem weaponItem:
                // Использование оружия

                break;

            case ToolItem toolItem:

                // Использование инструмента
                break;

            default:



                break;

        }
    }

    private void UseRestorativeHealthItem(RestorativeHealthItem restorativeHealthItem)
    {
        _health.Heal(restorativeHealthItem.restorativeHealth);
    }
}
