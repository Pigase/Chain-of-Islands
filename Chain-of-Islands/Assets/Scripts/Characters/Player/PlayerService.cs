using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public static class PlayerService
{
    public static UnityEngine.GameObject PlayerGameObject
    {
        get
        {
            if (_playerGameObject == null)
                FindPlayer();
            return _playerGameObject;
        }
    }

    private static UnityEngine.GameObject _playerGameObject;
    private static float _distanceToPlayer;

    public static float FindDistanceToPlayer(Vector3 objectPosition)
    {
        // Проверить, нашли ли объект, и затем использовать
        if (_playerGameObject == null)
        {
            Debug.LogWarning("Объект с тегом 'Player' не найден!");
            return _distanceToPlayer = Mathf.Infinity;
        }
        else
        {
            return _distanceToPlayer = Vector3.Distance(objectPosition, _playerGameObject.transform.position);
        }
    }

    private static void FindPlayer()
    {
        _playerGameObject = UnityEngine.GameObject.FindWithTag("Player");

        if (PlayerGameObject == null)
            Debug.LogError("Player not found!");
        else
            Debug.Log("Player service initialized!");
    }
}