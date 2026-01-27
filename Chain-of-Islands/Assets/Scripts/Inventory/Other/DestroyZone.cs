using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyZone : MonoBehaviour , IDropHandler
{
    [SerializeField] private SlotInfoFinder _slotInfoFinder;

    public event Action<int> OnItemDroppedInDestroyZone;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item In Destroy Zone");

        var droppItem = eventData?.pointerDrag?.GetComponent<UIInventorySlot>();

        if (droppItem != null)
        {
            int indexDroppItem = _slotInfoFinder.SlotIndexFind(droppItem);

            OnItemDroppedInDestroyZone?.Invoke(indexDroppItem);
        }
    }
}
