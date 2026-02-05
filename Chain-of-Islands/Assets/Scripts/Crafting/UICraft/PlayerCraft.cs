using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCraft : MonoBehaviour
{
    private string _playerStationId = "Non";
    private BuildingStationManager _buildingStationManager;
    private Station station;

    public event Action OnPlayerStationStarted;

    private void Start()
    {
        _buildingStationManager = GetComponent<BuildingStationManager>();
        station = _buildingStationManager.GetStation(_playerStationId);

        OnPlayerStationStarted?.Invoke();
    }


}
