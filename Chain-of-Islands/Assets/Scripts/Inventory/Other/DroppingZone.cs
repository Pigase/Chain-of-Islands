using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class DroppingZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private float radiusDrop;
    [SerializeField] private DroppingZone droppingZone;
    [SerializeField] private Transform itemDroppingPosition;
    [SerializeField] private SlotInfoFinder _slotInfoFinder;

    private SpawnItemWorldPrefab spawnItemWorldPrefab;

    public event Action<int> OnDroppedItemIndex;
    public event Action<Item,int> OnItemDropped;

    private void Start()
    {
        spawnItemWorldPrefab = GameManager.GetSystem<SpawnItemWorldPrefab>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item In DropZone");
        var droppItem = eventData?.pointerDrag?.GetComponent<UIInventorySlot>();

        if (droppItem != null && !_slotInfoFinder.IsEmptySlot(droppItem))
        {
            int indexDroppItem = _slotInfoFinder.SlotIndexFind(droppItem);
            Item item = _slotInfoFinder.ItemInSlot(droppItem);
            int itemCount = _slotInfoFinder.InventorySlotInUISlot(droppItem).itemCount;

            OnDroppedItemIndex?.Invoke(indexDroppItem);
            OnItemDropped?.Invoke(item,itemCount);

            Vector2 itemPos = Vector2.zero;
            for(int i = 0; i < itemCount; i++)
            {
                itemPos = CalculateDropPosition();
                spawnItemWorldPrefab.SpawnItem(item, itemPos);
            }
        }
    }

    private Vector2 CalculateDropPosition()
    {
        float xPos = Random.Range(-radiusDrop, radiusDrop);
        float yPos = Random.Range(-radiusDrop, radiusDrop);

        Vector2 itemPos = new Vector2(xPos,yPos);

        return itemPos;
    }
}
