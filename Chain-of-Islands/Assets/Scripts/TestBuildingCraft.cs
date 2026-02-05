using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuildingCraft : MonoBehaviour
{
    [SerializeField] private Station _station;

    public event Action<Station> OnTestChengedStation;

    public void TestChangedStation()
    {
        OnTestChengedStation.Invoke(_station);
    }
}
