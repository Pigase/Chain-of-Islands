using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Station", menuName = "Station")]
public class Station : ScriptableObject
{
    [Header("Ингредиенты для открытия станции")]
    [Tooltip("Что нужно для открытия ")]
    public List<ItemStack> ingredients = new List<ItemStack>();

    [Header("Basic Info")]
    public string StationId;

    [Header("Basic Info")]
    public string StationName;

    [Header("Description")]
    public string description;
}
