using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventary : MonoBehaviour
{
    public event Action<string,int> OnItemAdded;

    [SerializeField] private string itemId;
    [SerializeField] private int count;

    public void AddItem()
    {
        OnItemAdded?.Invoke(itemId, count);
    }
}
