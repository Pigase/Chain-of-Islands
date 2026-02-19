using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private HealthComponent _playerHealthComponent;
    [SerializeField] private GameObject _ReaspawnButton;
    [SerializeField] private Transform _respawnPosition;
    [SerializeField] private Joystick _joystick;

    public event Action OnPlayerRespawned;

    public void RespawnPlayer()
    {
        _playerHealthComponent.ResetHealth();
        OnPlayerRespawned?.Invoke();
        TranslatePlayer(_playerHealthComponent.gameObject);
        _joystick.gameObject.SetActive(true);
    }

    private void TranslatePlayer(GameObject player)
    {
        player.transform.position = _respawnPosition.position;
    }


    private void SwitchingObjectStates()
    {
        _joystick.gameObject.SetActive(false);
        _ReaspawnButton.SetActive(true);
    }

    private void OnEnable()
    {
        _playerHealthComponent.OnDeath += SwitchingObjectStates;
    }

    private void OnDisable()
    {
        _playerHealthComponent.OnDeath -= SwitchingObjectStates;
    }
}
