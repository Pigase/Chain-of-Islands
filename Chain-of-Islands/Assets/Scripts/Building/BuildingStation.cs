using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildingStation : MonoBehaviour
{
    [SerializeField] private Station _buildingStation;

    public Station buildingStation => _buildingStation;
}
