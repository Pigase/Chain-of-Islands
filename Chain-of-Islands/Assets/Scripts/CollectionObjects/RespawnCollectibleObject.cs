using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCollectibleObject : MonoBehaviour
{
    [SerializeField] private CollectibleObject _collectibleObject;
    [SerializeField] private int _respawnTime;

    private void RespawnObject()
    {
        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(_respawnTime);
        _collectibleObject.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _collectibleObject.OnCollected += RespawnObject;
    }
    private void OnDisable()
    {
        _collectibleObject.OnCollected -= RespawnObject;
    }
}
