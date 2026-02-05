using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BuildingStationManager : MonoBehaviour
{
    [Header("Все Building игры")]
    [SerializeField] private List<Station> m_StationList = new List<Station>();
    private Dictionary<string, Station> m_StationDictionary;

    public void InitializeStationBase()
    {
        m_StationDictionary = new Dictionary<string, Station>();

        foreach(var station in m_StationList)
        {
            if (station == null)
            {
                Debug.LogWarning("Обнаружен null station в базе данных!");
                continue;
            }

            if(m_StationDictionary.ContainsKey(station.StationId))
            {
                Debug.LogError($"Дубликат station предмета: {station.StationId}");
                continue;
            }

            if (string.IsNullOrEmpty(station.StationId))
            {
                Debug.LogWarning($"Station без ID: {station.StationId}");
                continue;
            }

            m_StationDictionary[station.StationId] = station;
        }
    }

    public void ReloadDatabase()
    {
        InitializeStationBase();
    }

    public void Initialize()
    {
        InitializeStationBase();
    }

    public Station GetStation(string StationId)
    {
        if (m_StationDictionary.ContainsKey(StationId))
            return m_StationDictionary[StationId];
        return null;
    }
}
