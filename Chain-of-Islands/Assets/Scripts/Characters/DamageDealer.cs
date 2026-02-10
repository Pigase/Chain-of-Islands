using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float _damage;
    private Item _item;
    

    public void SetParametrsToUseEquipment(float damage, Item item)
    {
        _damage = damage;
        _item = item;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DamageReceiver>(out var damageReceiver))
        {
            damageReceiver.TakeDamage(_damage, _item);
        }
    }
}
