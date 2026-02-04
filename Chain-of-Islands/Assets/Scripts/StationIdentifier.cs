using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationIdentifier : MonoBehaviour
{
    private Station _station;
    private Station _nonStation;

    public event Action<Station> OnStationChange;

    private void Start()
    {
        _nonStation.StationId = "Non";
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Building")
        {
            BuildingStation building = collision.gameObject.GetComponent<BuildingStation>();
            _station = _nonStation;

            OnStationChange?.Invoke(_station);
        }
    }
}
