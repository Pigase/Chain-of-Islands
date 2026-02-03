using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemDrop : MonoBehaviour
{
    [SerializeField] private DroppingZone droppingZone;
    [SerializeField] private float radiusDrop;
    [SerializeField] private Transform itemDroppingPosition;

    private SpawnItemWorldPrefab spawnItemWorldPrefab;

    private void Start()
    {
        spawnItemWorldPrefab = GameManager.GetSystem<SpawnItemWorldPrefab>();
    }

    private void Drop(Item item , int amount)
    {
        spawnItemWorldPrefab.SpawnItem(item, amount, itemDroppingPosition, radiusDrop);
    }

    private void OnEnable()
    {
        droppingZone.OnItemDropped += Drop;
    }

    private void OnDisable()
    {
        droppingZone.OnItemDropped -= Drop;
    }
}
