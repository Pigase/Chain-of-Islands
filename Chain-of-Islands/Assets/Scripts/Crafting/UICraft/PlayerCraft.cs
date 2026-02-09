using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCraft : MonoBehaviour
{
    [SerializeField] private RectTransform _content;

    private string _playerStationId = "Non";
    private BuildingStationManager _buildingStationManager;
    private Station station;

    public event Action<RectTransform, Station> OnPlayerStationStarted;

    private void Start()
    {
        _buildingStationManager = GameManager.GetSystem<BuildingStationManager>();
        station = _buildingStationManager.GetStation(_playerStationId);

        OnPlayerStationStarted?.Invoke(_content, station);
    }

    private void OnEnable()
    {
        OnPlayerStationStarted?.Invoke(_content, station);
    }
}
