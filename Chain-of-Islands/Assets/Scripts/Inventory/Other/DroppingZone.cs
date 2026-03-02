using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;

public class DroppingZone : MonoBehaviour
{
    [SerializeField] private float radiusDrop;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private SlotInfoFinder _slotInfoFinder;
    [SerializeField] private UIInventory _uiInventory;

    private InventarySystem _inventarySystem;
    private SpawnItemWorldPrefab spawnItemWorldPrefab;

    private void Start()
    {
        _inventarySystem = GameManager.GetSystem<InventarySystem>();
        spawnItemWorldPrefab = GameManager.GetSystem<SpawnItemWorldPrefab>();
    }
    private void ItemDrop(UIInventorySlot slot, Vector2 dropPos)
    {
        if (slot != null && !_slotInfoFinder.IsEmptySlot(slot))
        {
            int indexDroppItem = _slotInfoFinder.SlotIndexFind(slot);
            Item item = _slotInfoFinder.ItemInSlot(slot);
            int itemCount = _slotInfoFinder.InventorySlotInUISlot(slot).itemCount;

            _inventarySystem.RemoveItems(indexDroppItem);

            for (int i = 0; i < itemCount; i++)
            {
                Vector2 itemPos = CalculateDropPosition(dropPos);
                spawnItemWorldPrefab.SpawnItem(item, itemPos);
            }
        }
    }

    private Vector2 CalculateDropPosition(Vector2 dropPos)
    {

        Vector2 playerPos = _playerPosition.transform.position;
         
        Vector2 itemPos = Vector2.zero;

        if (Vector2.Distance(playerPos, dropPos) <= radiusDrop)
        {
            itemPos = dropPos;
            return itemPos;
        }
        itemPos = (dropPos - playerPos).normalized * radiusDrop + playerPos;

        return itemPos;
    }

    private void OnEnable()
    {
        _uiInventory.OnItemDropedOut += ItemDrop;
    }

    private void OnDisable()
    {
        _uiInventory.OnItemDropedOut -= ItemDrop;
    }
}
