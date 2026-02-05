using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationKeeper : MonoBehaviour
{
    [SerializeField] private TestBuildingCraft _testBuildingCraft;

    public Station station => _station;

    private Station _station;

    private void SetStation(Station station)
    {
        Debug.Log("SetTestStatuon");
        _station = station;
    }

    public void OnEnable()
    {
        _testBuildingCraft.OnTestChengedStation += SetStation;
    }

    public void OnDisable()
    {
        _testBuildingCraft.OnTestChengedStation -= SetStation;
    }
}
