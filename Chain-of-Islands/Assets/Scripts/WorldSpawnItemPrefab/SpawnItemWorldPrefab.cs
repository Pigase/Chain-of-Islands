using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnItemWorldPrefab : MonoBehaviour
{
    [SerializeField] private WorldPool _worldPool;

    public void Initialize()
    {

        if (_worldPool == null)
            throw new ArgumentNullException(nameof(_worldPool), "_worldPool cannot be null");

        _worldPool.Initialize();
    }

    public void SpawnItem(Item item,int amount, Transform itemDroppingPosition, float radiusDrop)
    {
        if(item != null)
        {
            for (int i = 0; i < amount; i++)
            {
                Debug.Log("CreatePrefab");
                var dropItem = _worldPool.GetFreeElement();
                dropItem.GetComponent<SelectableItem>().item = item;
                dropItem.SpriteRenderer.sprite = item.worldPrefabIcon;  
                DropPosition(itemDroppingPosition, dropItem, radiusDrop);
            }
        }
    }

    private void DropPosition(Transform itemDroppingPosition, ItemPrefab item, float radiusDrop)
    {
        item.transform.position = itemDroppingPosition.position;

        Vector2 randomOffset = Random.insideUnitCircle * radiusDrop;
        Vector3 dropPos = itemDroppingPosition.position + new Vector3(randomOffset.x, 1f, randomOffset.y);

        item.transform.position = dropPos;
    }
}
