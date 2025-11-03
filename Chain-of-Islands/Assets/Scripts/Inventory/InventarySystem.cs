using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventarySystem : MonoBehaviour
{
    [SerializeField] private TestInventary testInventary;

    [SerializeField]private Inventory inventory ;

    public void AddItems(string itemId, int count)
    {
        ItemDataBase itemData = GameManager.GetSystem<ItemDataBase>();
        var itemInfo = itemData.GetItem(itemId);

        if(inventory != null)
        {
            foreach (var item in inventory.slots)
            {


                if (item.itemId == itemId && item.itemCount < itemInfo.maxStackSize)
                {
                    int freeSpace = itemInfo.maxStackSize - item.itemCount;

                    if (freeSpace <= count)
                    {
                        item.itemCount = itemInfo.maxStackSize;
                        count -= freeSpace;
                        Debug.Log($"Добавлен предмет с id: {itemId} в количестве {freeSpace}");
                    }
                    else
                    {
                        item.itemCount += count;
                        Debug.Log($"Добавлен предмет с id: {itemId} в количестве {count}");
                    }
                }
            }
        }
        for (int i = 0, ostatok = count; i < inventory.slots.Count && ostatok > 0; i++)
        {
            

            if (inventory.slots[i].empty && ostatok > 0)
            {
                inventory.slots[i].itemId = itemId;
                inventory.slots[i].itemCount = ostatok;
                inventory.slots[i].empty = false;
                
                ostatok -= itemInfo.maxStackSize;
                Debug.Log($"Добавлен предмет с id: {itemId} в количестве {count}");
            }
            else
            {
                Debug.Log("не удалось добавить предмет");
            }

        }
        for (int j = 0; j < inventory.slots.Count; j++)
        {
            Debug.Log($"Ячейка  {j} пустой ли: {inventory.slots[j].empty} . Ячейка  {j} количество: {inventory.slots[j].itemCount}");
        }

    }
    public void RemoveItems(string itemId, int count)
    {
        for (int i = inventory.slots.Count - 1; i >= 0 && count > 0; i--)
        {
            var slot = inventory.slots[i];

            if (slot.itemId == itemId)
            {
                if (slot.itemCount <= count)
                {
                    count -= slot.itemCount;
                    inventory.slots.RemoveAt(i);
                    Debug.Log($"Удален предмет с id: {itemId} в количестве {slot.itemCount}. Осталось удалить: {count}");
                }
                else
                {
                    slot.itemCount -= count;
                    Debug.Log($"Удален предмет с id: {itemId} в количестве {count}. В слоте осталось: {slot.itemCount}");
                    count = 0;
                }
            }
        }

        if (count > 0)
        {
            Debug.LogWarning($"Не удалось удалить все предметы. Осталось удалить: {count}");
        }
    }

    public void OnEnable()
    {
        testInventary.OnItemAdded += AddItems;
    }
}
