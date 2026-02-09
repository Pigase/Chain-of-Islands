using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCraftPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _content;

    private Station _station;

    public event Action<RectTransform,Station> OnBuildPanelChanged;
    public event Func<Station> OnBuildPanelOnEnable;

    private void PanelChaged()
    {
        OnBuildPanelChanged?.Invoke(_content,_station);
    }

    private void GetStation(Station station)
    {
        Debug.Log("GetStation");
        if(station != null)
        {
            _station = station;
            Debug.Log($"Panel Script  Station: {station.StationId}");
            PanelChaged();
        }
    }

    private void OnEnable()
    {
        Debug.Log("Enable");
        _station = OnBuildPanelOnEnable?.Invoke();
        GetStation(_station);
    }
}
