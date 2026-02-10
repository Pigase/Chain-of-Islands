using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using static UnityEditor.Progress;

public class ItemUseHandler : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private DamageDealer _damageDealer;

    private ItemDataBase _itemData;
    private InventarySystem _inventarySystem;
    private InventorySlot _currentInventorySlot;
    private Item _currentItem;

    public Action<EquipmentItem> UseEquipment;

    private void Start()
    {
        _itemData = GameManager.GetSystem<ItemDataBase>();//надо обь€вл€ть в старте т.к. система только в awake создаетьс€
        _inventarySystem = GameManager.GetSystem<InventarySystem>();//надо обь€вл€ть в старте т.к. система только в awake создаетьс€
    }

    private void OnEnable()
    {
        _health.OnSubjectHealed += SearchForAUsedHealingItem;
    }

    private void OnDisable()
    {
        _health.OnSubjectHealed -= SearchForAUsedHealingItem;

    }

    public void ItemIdentification(InventorySlot slot)
    {
        if (slot.empty)
            return;

        _currentInventorySlot = slot;
        Item item = _itemData.GetItem(slot.itemId);

        _currentItem = item;

        switch (item)
        {
            case RestorativeHealthItem healthItem:

                UseRestorativeHealthItem(healthItem);

                break;

            case WeaponItem weaponItem:

                WeponItemIdentification(weaponItem);

                break;

            case ToolItem toolItem:

               ToolItemIdentification(toolItem);

                break;

            default:



                break;

        }
    }

    private void UseRestorativeHealthItem(RestorativeHealthItem restorativeHealthItem)
    {
        _health.HealFromSubject(restorativeHealthItem.restorativeHealth, restorativeHealthItem.healingRecharge);
    }

    private void WeponItemIdentification(WeaponItem weaponItem)
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

    private void ToolItemIdentification(ToolItem toolItem)
    {
        switch (toolItem)
        {
            case Axe axe:

                UseAxe(axe);

                break;

            case Pickaxe pickaxe:

                UsePickaxe(pickaxe);

                break;

            default:



                break;
        }
    }

    private void UseSword(Sword sword)
    {

        _damageDealer.SetParametrsToUseEquipment(sword.damage, sword);
        UseEquipment?.Invoke(sword);

    }

    private void UseAxe(Axe axe)
    {
        UseEquipment?.Invoke(axe);
        _damageDealer.SetParametrsToUseEquipment(axe.damage, axe);
    }

    private void UsePickaxe(Pickaxe pickaxe)
    {
        UseEquipment?.Invoke(pickaxe);
        _damageDealer.SetParametrsToUseEquipment(pickaxe.damage, pickaxe);

    }

    private void SearchForAUsedHealingItem(float AmountOfHealthHealed)
    {
        if (_currentItem == null)
        {
            Debug.LogWarning("Current item is null!");
            return;
        }

        if (_currentItem is RestorativeHealthItem)
        {
        _inventarySystem.SubstractItemFromSlot(_currentInventorySlot);
        }

        _currentItem = null;
        _currentInventorySlot = null;
    }

}
