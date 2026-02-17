using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationKeeper : MonoBehaviour
{
    [SerializeField] private StationIdentifier _stationIdentifier;
    [SerializeField] private BuildingCraftPanel _buildingCraftPanel;
    [SerializeField] private BuildingOpener _buildingOpener;

    public Station station => _station;

    private BuildingStation _building;
    private Station _station;

    private void SetBuilding(BuildingStation building)
    {
        _building = building;
    }

    private Station GiveStation()
    {
        _station = _building.buildingStation;
        return station;
    }
    private BuildingStation GiveBuilding()
    {
        return _building;
    }
    private void OnEnable()
    {
        _stationIdentifier.OnStationChange += SetBuilding;
        _buildingCraftPanel.OnBuildPanelOnEnable += GiveStation;
        _buildingOpener.OnRequestedBuilding += GiveBuilding;
    }

    private void OnDisable()
    {
        _buildingCraftPanel.OnBuildPanelOnEnable -= GiveStation;
        _stationIdentifier.OnStationChange -= SetBuilding;
        _buildingOpener.OnRequestedBuilding -= GiveBuilding;
    }
}
