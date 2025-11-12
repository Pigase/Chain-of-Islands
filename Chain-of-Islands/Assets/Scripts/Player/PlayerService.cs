using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerService
{
    public static GameObject PlayerGameObject
    {
        get
        {
            if (_playerGameObject == null)
                FindPlayer();
            return _playerGameObject;
        }
    }

    private static GameObject _playerGameObject;

    private static void FindPlayer()
    {
        _playerGameObject = GameObject.FindWithTag("Player");

        if (PlayerGameObject == null)
            Debug.LogError("Player not found!");
        else
            Debug.Log("Player service initialized!");
    }
}