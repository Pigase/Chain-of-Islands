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

    private void PanelChaged(Station station)
    {
        OnBuildPanelChanged?.Invoke(_content, station);
    }


    private void OnEnable()
    {
        Debug.Log("Enable");
        _station = OnBuildPanelOnEnable?.Invoke();
        PanelChaged(_station);
    }
}
