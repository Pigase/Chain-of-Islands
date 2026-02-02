using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [Header("Выпадающие предметы")]
    [Tooltip("ID предмета и его количество ")]
    [SerializeField] private List<ItemStack> dispensingItems = new List<ItemStack>();

    public List<ItemStack> DispensingItems => dispensingItems;
}
