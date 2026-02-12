using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ArmorItem;

public class UIInventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image _iconItem;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _itemsCount;

    public enum SlotType { ArmorSlot,InventarySlot, HotInventarySlot }
    public SlotType type { get; set; }
    public ArmorType armorType { get; set; }

    private ItemDataBase itemData;

    public event Action<UIInventorySlot, UIInventorySlot> OnItemDropped;
    public event Action<UIInventorySlot, UIInventorySlot> OnItemDroppedOnArmorSlot;
    public event Action<UIInventorySlot> OnSlotSelected;

    private void Awake()
    {
        itemData = GameManager.GetSystem<ItemDataBase>();
    }

    public void RefreshSlotUI(InventorySlot slot)
    {
        _iconItem.gameObject.SetActive(!slot.empty);
        _itemsCount.text = slot.empty || slot.itemCount <= 1 ? "" : slot.itemCount.ToString();

        if(!slot.empty )
        {
            var itemInfo = itemData?.GetItem(slot?.itemId);
            _iconItem.sprite = itemInfo?.icon;
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        UIInventorySlot draggedSlot = eventData.pointerDrag?.GetComponent<UIInventorySlot>();

        if (draggedSlot != null)
        {
            OnItemDropped?.Invoke(this, draggedSlot);
            OnSlotSelected?.Invoke(this);
            Debug.Log("Drop");
        }
    }

    public void SelectSlot(bool selected)
    {
        if (selected)
        {
            _icon.gameObject.SetActive(true);
        }
        else
        {
            _icon.gameObject.SetActive(false);
        }
    }
}
