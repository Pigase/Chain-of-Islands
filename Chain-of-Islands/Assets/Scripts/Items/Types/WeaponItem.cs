using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
    public class WeaponItem : Item, IPickupable
    {
    public override ItemType ItemType => ItemType.Weapon;

    [Header("WeaponItem Properties")]
        public int damage;
    }
