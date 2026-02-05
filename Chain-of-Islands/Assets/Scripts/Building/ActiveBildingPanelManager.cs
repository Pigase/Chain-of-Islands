using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBildingPanelManager : MonoBehaviour
{
    [SerializeField] private StationIdentifier _stationIdentifier;
    [SerializeField] private TestBuildingCraft _testBuildingCraft;
    [SerializeField] private GameObject _buildingCraftButton;

    private void ButtonActivate(Station station)
    {
        _buildingCraftButton.SetActive(true);
    }

    private void OnEnable()
    {
        _stationIdentifier.OnStationChange += ButtonActivate;
        _testBuildingCraft.OnTestChengedStation += ButtonActivate;
    }

    private void OnDisable()
    {
        _stationIdentifier.OnStationChange -= ButtonActivate;
        _testBuildingCraft.OnTestChengedStation -= ButtonActivate;
    }
}
