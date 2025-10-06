using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examination : MonoBehaviour
{
    [SerializeField] private string _itemId;
    [SerializeField] private string _itemTwoId;

    public void Exam()
    {
        if(ItemDataBase.Instance == null)
        {
            Debug.LogError("DataBase not initilizathion");
            return;
        }

        ItemDataBase.Instance.TestFindItem(_itemId);
        Item itemOne = ItemDataBase.Instance.GetItem(_itemId);
        Item itemTwo = ItemDataBase.Instance.GetItem(_itemTwoId);

        Debug.Log($"������� {itemOne.itemName} � id: {itemOne} ���� : {itemOne.GetItemType()}");
        Debug.Log($"������� {itemTwo.itemName} � id: {itemTwo} ���� : {itemTwo.GetItemType()}");
    }
}
