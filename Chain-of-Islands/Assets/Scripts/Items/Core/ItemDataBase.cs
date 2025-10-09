using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    [Header("��� �������� ����")]
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
                Debug.LogWarning("��������� null ������� � ���� ������!");
                skippedItems++;
                continue;
            }

            if (string.IsNullOrEmpty(item.itemId))
            {
                Debug.LogWarning($"������� ��� ID: {item.name}");
                skippedItems++;
                continue;
            }

            if (itemsMap.ContainsKey(item.itemId))
            {
                Debug.LogError($"�������� ID ��������: {item.itemId}");
                skippedItems++;
                continue;
            }

            itemsMap[item.itemId] = item;
        }

        Debug.Log($"ItemDatabase ���������������. ���������: {itemsMap.Count}, ���������: {skippedItems}");
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
                Debug.Log("������� ������ " + foundItem.itemId + " �������� " + foundItem.itemName);
            }
            else
            {
                Debug.LogWarning($"�� ������� ����� ������� � ID: {itemIdToTest} ��� ������ ������.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"������ ��� ������ �������� {itemIdToTest}: �������� � ����� id ���");
        }
        
    }
        
}
