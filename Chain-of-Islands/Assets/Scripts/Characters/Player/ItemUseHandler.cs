using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemUseHandler : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private DamageDealer _damageDealer;

    private ItemDataBase _itemData;

    public Action<WeaponItem> Attack;

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

                UseWeponItem(weaponItem);

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

    private void UseWeponItem (WeaponItem weaponItem)
    {
        switch (weaponItem)
        {
            case Sword sword:

                UseSword(sword);

                break;

            default:



                break;
        }
    }

    private void UseSword (Sword sword)
    {

        _damageDealer.SetDamage(sword.damage);
        Attack?.Invoke(sword);

    }
}
