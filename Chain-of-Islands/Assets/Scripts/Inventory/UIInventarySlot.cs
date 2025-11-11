using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIInventarySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image iconItem;
    [SerializeField] private TextMeshProUGUI itemsCount;

    private ItemDataBase itemData;

    public event Func<UIInventarySlot, int> IItemDrop; 
    public event Action<int, int> IItemSwap; 
    public event Action ISwap; 
    private void Start()
    {
        itemData = GameManager.GetSystem<ItemDataBase>();
    }
    public void RefreshSlotUI(InventorySlot slot)
    {
        if (slot != null)
        {
            if (slot.empty)
            {
                iconItem.gameObject.SetActive(false);
                itemsCount.text = "";

                Debug.Log("SlotEmpty");
            }
            if (!slot.empty)
            {
                var itemInfo = itemData.GetItem(slot?.itemId);


                iconItem.gameObject.SetActive(true);

                iconItem.sprite = itemInfo.icon;

                if(slot.itemCount > 1)
                {
                    itemsCount.text = slot.itemCount.ToString();
                }
                Debug.Log("SlotNotEmpty");
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedItem = eventData.pointerDrag;

        if (draggedItem != null)
        {
            int Oneitem = (int)IItemDrop?.Invoke(gameObject?.GetComponent<UIInventarySlot>());
            int Twoitem = (int)IItemDrop?.Invoke(draggedItem?.GetComponent<UIInventarySlot>());

            Debug.Log($"Index gameobj : {Oneitem}");
            Debug.Log($"Index dragged : {Twoitem}");

            if(Oneitem >= 0 && Twoitem >= 0)
            {
                IItemSwap?.Invoke(Oneitem, Twoitem);
                ISwap?.Invoke();
            }

            Debug.Log("DropItem");
        }
    }
}
