using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Armor" , menuName = "Items/Armor")]
public class ArmorItem : Item, IPickupable
{
    public override ItemType ItemType => ItemType.Armor;

    [Header("Armor Properties")]
    public int armor;

}
