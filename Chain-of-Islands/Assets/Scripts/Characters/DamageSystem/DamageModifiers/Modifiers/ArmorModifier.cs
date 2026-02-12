using System.Collections.Generic;
using UnityEngine;

public class ArmorModifier : MonoBehaviour, IDamageModifier
{
    [Header("Настройки брони")]
    [SerializeField] private float _armor = 10f;
    [SerializeField] private bool _isPlayer = false;

    private InventarySystem _inventarySystem;
    private ItemDataBase _itemData;
    private List <InventorySlot> _inventorySlots;
  //  private List <InventorySlot> _inventorySlots;
    private int _priority = 100; // Применяется после щитов, до иммунитетов

    public int Priority => _priority;

    private void Start()
    {
        _inventarySystem = GameManager.GetSystem<InventarySystem>();
        _itemData = GameManager.GetSystem<ItemDataBase>();
    }

    public float ModifyDamage(float incomingDamage)
    {
        if(_isPlayer)
        {
        CalculateArmor();
        }

        float resistance = _armor;
        float reducedDamage = incomingDamage - resistance;

        Debug.Log($"Armor reduced damage: {incomingDamage} → {reducedDamage} (-{resistance})");

        return reducedDamage;
    }

    private float CalculateArmor()
    {
        _armor = 0;

        _inventorySlots = _inventarySystem.GetArmorSlots();
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

        return _armor;
    }
}