using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private ZoneEntryIdentifier _zoneEntryIdentifier;
    [SerializeField] private GameObject _collectibleButton;
    [SerializeField] private Collector _collector;

    private void Start()
    {
    }

    private void ActiveButton(bool IsEntered)
    {
        _collectibleButton.SetActive(IsEntered);
    }

    public void ActiveCollector()
    {
        _collector.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _zoneEntryIdentifier.IsZoneEntered += ActiveButton;
    }

    private void OnDisable()
    {
        _zoneEntryIdentifier.IsZoneEntered -= ActiveButton;
    }
}
