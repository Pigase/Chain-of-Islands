using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Игровые системы")]
    public ItemDataBase itemDataBase;
    public CraftingSystem craftingSystem;
    public SpawnItemWorldPrefab spawnItemWorldPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeGame();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeGame()
    {
        itemDataBase.Initialize();
        craftingSystem.Initialize(itemDataBase);
        spawnItemWorldPrefab.Initialize();

        Debug.Log("Все системы инициализированы!");
    }

    public static T GetSystem<T>() where T : Component
    {
        if (Instance == null) return null;
        if (typeof(T) == typeof(ItemDataBase)) return Instance.itemDataBase as T;
        if (typeof(T) == typeof(CraftingSystem)) return Instance.craftingSystem as T;
        if (typeof(T) == typeof(SpawnItemWorldPrefab)) return Instance.spawnItemWorldPrefab as T;
        
        return null;
    }
}
