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

    public void SpawnItem(Item item, Vector2 spawnPosition)
    {
        if(item != null)
        {
            Debug.Log("CreatePrefab");
            var dropItem = _worldPool.GetFreeElement();
            dropItem.GetComponent<SelectableItem>().item = item;
            dropItem.SpriteRenderer.sprite = item.worldPrefabIcon;
            dropItem.transform.position = spawnPosition;
        }
    }
}
