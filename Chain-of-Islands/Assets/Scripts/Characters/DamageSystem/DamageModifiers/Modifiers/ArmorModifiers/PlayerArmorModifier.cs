using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmorModifier : ArmorModifierBase
{
    [Header("Настройки брони")]
    [SerializeField] private float _armor = 0;

    private InventarySystem _inventarySystem;
    private ItemDataBase _itemData;
    private List<InventorySlot> _inventorySlots;

    private void Awake()
    {
        _inventarySystem = GameManager.GetSystem<InventarySystem>();
        _itemData = GameManager.GetSystem<ItemDataBase>();
    }

    private void OnEnable()
    {
        _inventarySystem.OnArmorSlotChanged += CalculateArmor;
    }

    private void OnDisable()
    {
        _inventarySystem.OnArmorSlotChanged -= CalculateArmor;
    }

    public override float ModifyDamage(float incomingDamage)
    {
        float resistance = _armor;
        float reducedDamage = Mathf.Max(0, incomingDamage - resistance); // защита от отрицательного урона

        Debug.Log($"Armor absorbed: {Mathf.Min(incomingDamage, resistance)} damage");
        return reducedDamage;
    }

    private void CalculateArmor(List<InventorySlot> inventorySlots)
    {

        Debug.Log(inventorySlots.Count+"   ddddddddd");

        _armor = 0;

        _inventorySlots = inventorySlots;
        Item item;

        foreach (var slot in _inventorySlots)
        {
            if (slot.empty)
                continue;

            item = _itemData.GetItem(slot.itemId);

            if (item is ArmorItem armor)
            {
                _armor += armor.armor;
            }
        }
    }
}
