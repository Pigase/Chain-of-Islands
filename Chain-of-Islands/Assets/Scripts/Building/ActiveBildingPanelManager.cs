using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBildingPanelManager : MonoBehaviour
{
    [SerializeField] private StationIdentifier _stationIdentifier;
    [SerializeField] private UnityEngine.GameObject _buildingCraftButton;

    private void ButtonActivate()
    {
        _buildingCraftButton.SetActive(true);
    }
    private void ButtonNotActivate()
    {
        _buildingCraftButton.SetActive(false);
    }
    private void OnEnable()
    {
        _stationIdentifier.OnEnterFromBuildingTrigger += ButtonActivate;
        _stationIdentifier.OnExitedFromBuildingTrigger += ButtonNotActivate;
    }

    private void OnDisable()
    {
        _stationIdentifier.OnEnterFromBuildingTrigger -= ButtonActivate;
        _stationIdentifier.OnExitedFromBuildingTrigger -= ButtonNotActivate;
    }
}
