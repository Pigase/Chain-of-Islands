using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConsumableItem", menuName = "Items/ConsumableItem")]
public class RestorativeHealthItem : Item, IPickupable
{
    public override ItemType ItemType => ItemType.Health;

    [Header("Restorative Health Item Properties")]
    public float restorativeHealth;
    public int coolDownHealth;
}
