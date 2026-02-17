using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationIdentifier : MonoBehaviour
{
    private BuildingStation _building;

    public event Action<BuildingStation> OnStationChange;
    public event Action<bool> IsEnterFromBuildingTrigger;
    public event Action<bool> IsEnterFromCloseBuildingTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Building"))
        {
            BuildingStation building = collision.gameObject.GetComponent<BuildingStation>();
            _building = building;
            OnStationChange?.Invoke(_building);

            if (building.IsBuildingOpen)
            {
                Debug.Log("true");
                IsEnterFromBuildingTrigger?.Invoke(true);
            }
            if(!building.IsBuildingOpen)
            {
                Debug.Log("false");
                IsEnterFromCloseBuildingTrigger?.Invoke(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Building"))
        {
            BuildingStation building = collision.gameObject.GetComponent<BuildingStation>();
            IsEnterFromBuildingTrigger?.Invoke(false);
            IsEnterFromCloseBuildingTrigger?.Invoke(false);
        }
    }
}
