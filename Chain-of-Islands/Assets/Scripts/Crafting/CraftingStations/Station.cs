using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Station", menuName = "Station")]
public class Station : ScriptableObject
{
    [Header("Basic Info")]
    public string StationId;

    [Header("Basic Info")]
    public string StationName;

    [Header("Description")]
    public string description;
}
