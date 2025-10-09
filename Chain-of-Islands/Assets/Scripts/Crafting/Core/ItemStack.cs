using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    [Tooltip("ID предмета из ItemDataBase")]
    public string itemId;

    [Tooltip("Количество")]
    public int amount;
}
