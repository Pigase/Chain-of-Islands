using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventary : MonoBehaviour
{
    public event Action<string,int> OnItemAdded;
    public event Action<string,int> OnItemRemove;
    public event Action OnItemTest;

    [SerializeField] private string itemIdAdd;
    [SerializeField] private string itemIdRemove;
    [SerializeField] private int countAdd;
    [SerializeField] private int countRemove;

    public void AddItem()
    {
        OnItemAdded?.Invoke(itemIdAdd, countAdd);
        OnItemRemove?.Invoke(itemIdRemove, countRemove);
        OnItemTest?.Invoke();
    }
}
