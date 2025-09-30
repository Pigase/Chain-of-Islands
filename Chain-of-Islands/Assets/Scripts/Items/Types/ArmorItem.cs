using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Armor" , menuName = "Items/Armor")]
public class ArmorItem : Item
{
    [Header("Armor Properties")]
    public ArmorType armorType;

    public ArmorItem()
    {
        itemType = ItemType.Armor;
    }
}
