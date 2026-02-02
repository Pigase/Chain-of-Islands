using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ItemsDrop : MonoBehaviour
{
    [SerializeField] private DroppingZone droppingZone;

    private ItemDataBase itemDate;
    private WorldPool _pool;

    private void Start()
    {
        itemDate = GameManager.GetSystem<ItemDataBase>();
        _pool = GameManager.GetSystem<WorldPool>();
    }

    private void DroppingItems(Item item, int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            var dropItem = _pool.GetFreeElement();
            dropItem.SpriteRenderer.sprite = item.worldPrefabIcon;
            DropPosition(transform,dropItem,2);
        }        
    }

    private void DropPosition(Transform itemDroppingPosition, ItemPrefab item, int radiusDrop)
    {
        Vector3 highPos = new Vector3(itemDroppingPosition.position.x, itemDroppingPosition.position.y + 2, itemDroppingPosition.position.z);
        Vector3 endPos = new Vector3(itemDroppingPosition.position.x + Random.RandomRange(1, radiusDrop), itemDroppingPosition.position.y + Random.RandomRange(1, radiusDrop), itemDroppingPosition.position.z);
        item.transform.position = itemDroppingPosition.position;
    }

    private void OnEnable()
    {
        droppingZone.OnItemDropped += DroppingItems;
    }

    private void OnDisable()
    {
        droppingZone.OnItemDropped -= DroppingItems;
    }
}
