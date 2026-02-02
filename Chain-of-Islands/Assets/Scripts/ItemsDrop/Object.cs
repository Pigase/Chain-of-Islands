using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [Header("Выпадающие предметы")]
    [Tooltip("Предмет и его количество ")]
    [SerializeField] private List<StackDiscardedItems> dispensingItems = new List<StackDiscardedItems>();

    [SerializeField] private DropSystem dropSystem;
    public List<StackDiscardedItems> DispensingItems => dispensingItems;

    private List<StackDiscardedItems> SetItemsInfo()
    {
        return dispensingItems;
    }

    private void OnEnable()
    {
        dropSystem.OnRequestedForInformation += SetItemsInfo;
    }

    private void OnDisable()
    {
        dropSystem.OnRequestedForInformation -= SetItemsInfo;
    }
}
