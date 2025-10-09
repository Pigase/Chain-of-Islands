using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    [Header("Все предметы игры")]
    [SerializeField] private List<Item> allItems = new List<Item>();

    private Dictionary<string, Item> itemsMap;

    public void Initialize()
    {
        InitializeDatabase(); 
    }

    private void InitializeDatabase()
    {
        itemsMap = new Dictionary<string, Item>();
        int skippedItems = 0;

        foreach (var item in allItems)
        {
            if (item == null)
            {
                Debug.LogWarning("Обнаружен null предмет в базе данных!");
                skippedItems++;
                continue;
            }

            if (string.IsNullOrEmpty(item.itemId))
            {
                Debug.LogWarning($"Предмет без ID: {item.name}");
                skippedItems++;
                continue;
            }

            if (itemsMap.ContainsKey(item.itemId))
            {
                Debug.LogError($"Дубликат ID предмета: {item.itemId}");
                skippedItems++;
                continue;
            }

            itemsMap[item.itemId] = item;
        }

        Debug.Log($"ItemDatabase инициализирован. Загружено: {itemsMap.Count}, Пропущено: {skippedItems}");
    }

    public void ReloadDatabase()
    {
        InitializeDatabase();
    }

    public Item GetItem(string itemId)
    {
        if (itemsMap.ContainsKey(itemId))
            return itemsMap[itemId];

        return null;
    }

    public bool ItemExists(string itemId)
    {
        return itemsMap.ContainsKey(itemId);
    }

    public List<Item> GetItemsByType(System.Type itemType)
    {
        List<Item> itemsByType = new List<Item>();

        foreach (var item in allItems)
        {
            if(item.GetType() == itemType || item.GetType().IsSubclassOf(itemType))
            {
                itemsByType.Add(item);
            }
        }
        return itemsByType;
    }

    public void TestFindItem(string itemIdToTest)
    {
        try
        {
            Item foundItem = GetItem(itemIdToTest);

            if (foundItem != null)
            {
                Debug.Log("предмет найден " + foundItem.itemId + " название " + foundItem.itemName);
            }
            else
            {
                Debug.LogWarning($"Не удалось найти предмет с ID: {itemIdToTest} для выдачи игроку.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Ошибка при выдаче предмета {itemIdToTest}: предметв с таким id нет");
        }
        
    }
        
}
