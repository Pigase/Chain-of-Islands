using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("������� �������")]
    public ItemDataBase itemDataBase;
    public CraftingSystem craftingSystem;

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
        craftingSystem.Initialize(itemDataBase);;

        Debug.Log("��� ������� ����������������!");
    }

    public static T GetSystem<T>() where T : Component
    {
        if (Instance == null) return null;

        if (typeof(T) == typeof(ItemDataBase)) return Instance.itemDataBase as T;
        if (typeof(T) == typeof(CraftingSystem)) return Instance.craftingSystem as T;

        return null;
    }
}
