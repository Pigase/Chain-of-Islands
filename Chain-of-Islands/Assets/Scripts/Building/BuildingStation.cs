using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildingStation : MonoBehaviour
{
    [SerializeField] private Station _buildingStation;

    public bool IsBuildingOpen = false;

    public Station buildingStation => _buildingStation;

    public event Action OnBuildingOpened;
    public void BuildingOpen()
    {
        IsBuildingOpen = true;
        OnBuildingOpened?.Invoke();
    }
}
