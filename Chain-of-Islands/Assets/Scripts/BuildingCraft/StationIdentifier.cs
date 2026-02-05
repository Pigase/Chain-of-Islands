using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationIdentifier : MonoBehaviour
{
    private Station _station;
    private BuildingStationManager _buildingStationManager;

    public event Action<Station> OnStationChange;

    private void Start()
    {
        _buildingStationManager = GameManager.GetSystem<BuildingStationManager>();
        _station = _buildingStationManager.GetStation("Non");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Building")
        {
            BuildingStation building = collision.gameObject.GetComponent<BuildingStation>();
            _station = building.buildingStation;

            OnStationChange?.Invoke(_station);
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Building")
        {
            BuildingStation building = collision.gameObject.GetComponent<BuildingStation>();

            OnStationChange?.Invoke(_station);
        }
    }*/
}
