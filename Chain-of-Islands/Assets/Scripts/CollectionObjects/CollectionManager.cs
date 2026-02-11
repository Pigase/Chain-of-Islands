using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private GameObject _collectibleButton;

    private void ActiveButton(bool IsEntered)
    {
        _collectibleButton.SetActive(IsEntered);
    }

    private void OnEnable()
    {
        _collector.IsZoneEntered += ActiveButton;
    }

    private void OnDisable()
    {
        _collector.IsZoneEntered -= ActiveButton;
    }
}
