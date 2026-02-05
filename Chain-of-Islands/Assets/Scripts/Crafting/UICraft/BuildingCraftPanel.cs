using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCraftPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private StationKeeper _stationKeeper;

    private Station _station;
    private bool active = false;

    public event Action<RectTransform,Station> OnBuildPanelChanged;

    private void PanelChaged()
    {
        OnBuildPanelChanged?.Invoke(_content,_station);
    }

    private void GetStation(Station station)
    {
        Debug.Log("GetStation");
        if(_station == null || _station !=  station)
        {
            _station = station;
            Debug.Log($"Panel Script  Station: {station}");
            PanelChaged();
        }
    }

    private void OnEnable()
    {
        Debug.Log("Enable");
        GetStation(_stationKeeper.station);
    }
}
