using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBildingPanelManager : MonoBehaviour
{
    [SerializeField] private StationIdentifier _stationIdentifier;
    [SerializeField] private BuildingOpener _buildingOpener;
    [SerializeField] private UnityEngine.GameObject _buildingCraftButton;
    [SerializeField] private UnityEngine.GameObject _buildingOpenerButton;
    [SerializeField] private UnityEngine.GameObject _buildingOpenerPanel;

    private void CraftButtonActivate(bool active)
    {
        _buildingCraftButton.SetActive(active);
    }

    private void  OpenerButtonActivate(bool active)
    {
        _buildingOpenerButton.SetActive(active);
    }

    private void PanelInstallation()
    {
        _buildingCraftButton.gameObject.SetActive(true);
        _buildingOpenerButton.gameObject.SetActive(false);
        _buildingOpenerPanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _stationIdentifier.IsEnterFromBuildingTrigger += CraftButtonActivate;
        _stationIdentifier.IsEnterFromCloseBuildingTrigger += OpenerButtonActivate;
        _buildingOpener.OnOpenedBuilding += PanelInstallation;
    }

    private void OnDisable()
    {
        _stationIdentifier.IsEnterFromBuildingTrigger -= CraftButtonActivate;
        _stationIdentifier.IsEnterFromCloseBuildingTrigger -= OpenerButtonActivate;
        _buildingOpener.OnOpenedBuilding -= PanelInstallation;
    }
}
