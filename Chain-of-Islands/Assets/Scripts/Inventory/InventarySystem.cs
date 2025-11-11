using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEditor.Progress;

public class InventarySystem : MonoBehaviour
{
    [SerializeField] private TestInventary testInventary;
    [SerializeField] private UIInventary uiInventary;
    [SerializeField]private Inventory inventory ;

    public event Action IInventaryChange;

    public void AddItems(string itemId, int count)
    {
        // Проверяем, что передан корректный идентификатор предмета
        if (itemId != null)
        {
            // Получаем базу данных предметов из системы GameManager
            ItemDataBase itemData = GameManager.GetSystem<ItemDataBase>();
            // Получаем информацию о предмете по его ID
            var itemInfo = itemData.GetItem(itemId);

            // Первый проход: пытаемся добавить к существующим стекам предмета
            foreach (var item in inventory.slots)
            {
                // Если найден слот с таким же itemId и в нем есть свободное место
                if (item.itemId == itemId && item.itemCount < itemInfo.maxStackSize)
                {
                    // Вычисляем свободное место в слоте
                    int freeSpace = itemInfo.maxStackSize - item.itemCount;

                    // Если свободного места меньше или равно количеству для добавления
                    if (freeSpace <= count)
                    {
                        // Заполняем слот до максимума
                        item.itemCount = itemInfo.maxStackSize;
                        // Уменьшаем оставшееся количество для добавления
                        count -= freeSpace;
                        Debug.Log($"Добавлен предмет с id: {itemId} в количестве {freeSpace}");
                        IInventaryChange?.Invoke();
                    }
                    else
                    {
                        // Добавляем все оставшиеся предметы в этот слот
                        item.itemCount += count;
                        Debug.Log($"Добавлен предмет с id: {itemId} в количестве {count}");
                        // Обнуляем счетчик, так как все предметы добавлены
                        count = 0;
                        IInventaryChange?.Invoke();
                    }
                }
            }

            // Второй проход: создаем новые стеки для оставшихся предметов
            // Проходим по всем слотам инвентаря, пока есть предметы для добавления
            for (int i = 0, ostatok = count; i < inventory.slots.Count && ostatok > 0; i++)
            {
                // Если слот пустой
                if (inventory.slots[i].empty)
                {
                    // Определяем количество для добавления (минимум из оставшегося количества и максимального размера стека)
                    int addAmount = Mathf.Min(ostatok, itemInfo.maxStackSize);

                    // Заполняем слот
                    inventory.slots[i].itemId = itemId;
                    inventory.slots[i].itemCount = addAmount;
                    inventory.slots[i].empty = false;

                    // Уменьшаем оставшееся количество
                    ostatok -= addAmount;
                    IInventaryChange?.Invoke();
                    Debug.Log($"Добавлен предмет с id: {itemId} в количестве {addAmount}");
                }
                else
                {
                    Debug.Log("не удалось добавить предмет");
                }
            }
        }
    }

    public void RemoveItems(string itemId, int count)
    {
        // Проверяем, что передан корректный идентификатор предмета
        if (itemId != null)
        {
            // Проходим по слотам инвентаря с конца (для оптимизации при удалении)
            for (int i = inventory.slots.Count - 1; i >= 0 && count > 0; i--)
            {
                var slot = inventory.slots[i];

                // Если в слоте найден нужный предмет
                if (slot.itemId == itemId)
                {
                    // Если количество предметов в слоте меньше или равно требуемому для удаления
                    if (slot.itemCount <= count)
                    {
                        // Уменьшаем общее количество для удаления
                        count -= slot.itemCount;
                        // Очищаем слот
                        inventory.slots[i].itemId = null;
                        inventory.slots[i].empty = true;
                        inventory.slots[i].itemCount = 0;

                        IInventaryChange?.Invoke();

                        Debug.Log($"Удален предмет с id: {itemId} в количестве {slot.itemCount}. Осталось удалить: {count}");
                    }
                    else
                    {
                        // Удаляем только часть предметов из слота
                        slot.itemCount -= count;
                        Debug.Log($"Удален предмет с id: {itemId} в количестве {count}. В слоте осталось: {slot.itemCount}");
                        // Обнуляем счетчик, так как все предметы удалены
                        count = 0;
                        IInventaryChange?.Invoke();
                    }
                }
            }

            // Если после прохода по всем слотам остались предметы для удаления
            if (count > 0)
            {
                Debug.LogWarning($"Не удалось удалить все предметы. Осталось удалить: {count}");
            }
        }
    }

    public void ItemSwap(int indexOneSlot,int indexTwoSlot)
    {
        var item = inventory.slots[indexOneSlot];
        inventory.slots[indexOneSlot] = inventory.slots[indexTwoSlot];
        inventory.slots[indexTwoSlot] = item;
        Debug.Log("-------Swap Functional---------");
    }

    public void DebugSlotInfo()
    {
        for (int j = 0; j < inventory.slots.Count; j++)
        {
            Debug.Log($"Ячейка  {j} пустой ли: {inventory.slots[j].empty} . Ячейка  {j} количество: {inventory.slots[j].itemCount}");
        }
    }
    public void OnEnable()
    {
        testInventary.OnItemAdded += AddItems;
        testInventary.OnItemRemove += RemoveItems;
        testInventary.OnItemTest += DebugSlotInfo;
        testInventary.OnItemTest += DebugSlotInfo;
        uiInventary.KnowSwapItemIndex += ItemSwap;
    }
}
