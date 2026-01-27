using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppingZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private SlotInfoFinder _slotInfoFinder;

    public event Action<int> OnItemDroppedInDroppZone;
    public event Action<Item,int> OnItemDropped;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item In DropZone");
        var droppItem = eventData?.pointerDrag?.GetComponent<UIInventorySlot>();

        if (droppItem != null)
        {
            int indexDroppItem = _slotInfoFinder.SlotIndexFind(droppItem);
            Item item = _slotInfoFinder.ItemInSlot(droppItem);
            int itemCount = _slotInfoFinder.InventorySlotInUISlot(droppItem).itemCount;

            OnItemDroppedInDroppZone?.Invoke(indexDroppItem);
            OnItemDropped?.Invoke(item,itemCount);
        }
    }
}
