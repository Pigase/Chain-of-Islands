using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private WorldItemDrop _itemDrop;
    private void OnEnable()
    {
        _itemDrop.ResourcesAreSpawned += Deth;
    }

    private void OnDisable()
    {
        _itemDrop.ResourcesAreSpawned -= Deth;
    }

    private void Deth()
    {
        gameObject.SetActive(false);
    }
}
