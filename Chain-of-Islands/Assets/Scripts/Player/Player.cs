using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick _joystick; // Ссылка на компонент джойстика
    [SerializeField] private PlayerVisualStateMachine _playerVisualStateMachine;
    [SerializeField] private HealthComponent _health;
    [SerializeField] private InteractionJoystick _interactionJoystick;
    [SerializeField] private ItemUseHandler _itemUseHandler;
    [SerializeField] private float _speedPlayer;

    private Vector2 _moveDirectionPlayer; // Направление движения от джойстика
    private Mover _mover; // Кастомный класс для перемещения
    private Rigidbody2D _rb;

    public Action<float> PlayerGetDamage;

    private void Awake()
    {
        // Автоматически ищем HealthComponent если не назначен
        if (_health == null)
            _health = GetComponent<HealthComponent>();

        if (_health == null)
        {
            // Создаем автоматически если забыли добавить
            _health = gameObject.AddComponent<HealthComponent>();
        }

        // Создаем экземпляр Mover, передавая текущий GameObject
        _mover = new Mover(gameObject);
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Получаем направление движения от джойстика
        _moveDirectionPlayer = _joystick.FindingDirection();

        // Передаем направление в Mover для перемещения объекта
        _mover.MoveObjectInDerection(_speedPlayer, _moveDirectionPlayer, _rb);

        //Передаем направление в PlayerVisualStateMachine дял верного отображение спрайтов и выбора состояния
        _playerVisualStateMachine.ChooseState(_moveDirectionPlayer);
    }

    private void OnEnable()
    {
        _health.OnDamageTaken += TakeDamage;
        _interactionJoystick.ButtonPressed += HandleItemUse;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= TakeDamage;
        _interactionJoystick.ButtonPressed -= HandleItemUse;
    }

    private void HandleItemUse(InventorySlot slot)
    {
        _itemUseHandler.UseItem(slot);
    }
    private void TakeDamage(float damage, float timeInvulnerability)
    {
        PlayerGetDamage?.Invoke(timeInvulnerability);
    }
}