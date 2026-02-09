using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationKeeper : MonoBehaviour
{
    [SerializeField] private StationIdentifier _stationIdentifier;
    [SerializeField] private BuildingCraftPanel _buildingCraftPanel;

    public Station station => _station;

    private Station _station;

    private void SetStation(Station station)
    {
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
    }

    public void OnDisable()
    {
        _buildingCraftPanel.OnBuildPanelOnEnable -= GiveStation;
        _stationIdentifier.OnStationChange -= SetStation;
    }
}
