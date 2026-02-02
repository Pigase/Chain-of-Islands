using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] private Object _object;
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpande;
    [SerializeField] private ItemPrefab _prefab;

    private ItemDataBase itemDate;
    private WorldPool _pool;

    private void Start()
    {
        _pool = GameManager.GetSystem<WorldPool>();
    }

    private void DroppingItems(Item item, int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            var dropItem = _pool.GetFreeElement();
            dropItem.SpriteRenderer.sprite = item.worldPrefabIcon;
        }
    }
}
