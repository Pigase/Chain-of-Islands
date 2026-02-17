using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOpener : MonoBehaviour
{
    [SerializeField] private GameObject _openBuilding;

    private void Start()
    {
    }
    private void OpenBuilding()
    {
        _openBuilding.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
