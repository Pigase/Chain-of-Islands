using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationKeeper : MonoBehaviour
{
    [SerializeField] private TestBuildingCraft _testBuildingCraft;
    [SerializeField] private StationIdentifier _stationIdentifier;
    [SerializeField] private BuildingCraftPanel _buildingCraftPanel;

    public Station station => _station;

    private Station _station;

    private void SetStation(Station station)
    {
        Debug.Log("SetTestStatuon");
        _station = station;
    }

    private Station GiveStation()
    {
        return _station;
    }

    public void OnEnable()
    {
        _stationIdentifier.OnStationChange += SetStation;
        _buildingCraftPanel.OnBuildPanelOnEnable += GiveStation;
        _testBuildingCraft.OnTestChengedStation += SetStation;
    }

    public void OnDisable()
    {
        _buildingCraftPanel.OnBuildPanelOnEnable -= GiveStation;
        _testBuildingCraft.OnTestChengedStation -= SetStation;
        _stationIdentifier.OnStationChange -= SetStation;
    }
}
