using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [Header("Выпадающие предметы")]
    [Tooltip("Предмет и его количество ")]
    [SerializeField] private List<StackDiscardedItems> dispensingItems = new List<StackDiscardedItems>();
    [SerializeField] private WorldItemDrop _worldItemDrop;

    public List<StackDiscardedItems> DispensingItems => dispensingItems;

    private List<StackDiscardedItems> SetItemsInfo()
    {
        return dispensingItems;
    }

    private void OnEnable()
    {
        _worldItemDrop.OnRequestedForInformation += SetItemsInfo;
    }

    private void OnDisable()
    {
        _worldItemDrop.OnRequestedForInformation -= SetItemsInfo;
    }
}
