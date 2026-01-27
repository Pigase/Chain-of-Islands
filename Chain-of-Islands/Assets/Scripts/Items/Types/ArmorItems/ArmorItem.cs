using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIInventorySlot;

[CreateAssetMenu(fileName ="New Armor" , menuName = "Items/Armor")]
public class ArmorItem : Item
{
    public override ItemType ItemType => ItemType.Armor;
    public enum ArmorType {NoN,Helmet, Breastplate, Leggings }

    [Header("Armor Properties")]
    public int armor;
    public  ArmorType armorType;

    public override Type GetItemType() => typeof(ArmorItem);
}
