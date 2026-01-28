using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick _joystick; // Ссылка на компонент джойстика
    [SerializeField] private PlayerVisualStateMachine _playerVisualStateMachine;
    [SerializeField] private HealthComponent _health;
    [SerializeField] private float _speedPlayer;
    [SerializeField] private float _timeInvulnerability;
    [SerializeField] private bool _invulnerability = false;


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            if (collision.TryGetComponent<MobAI>(out var mobAI))
            {
                TakeDamage(mobAI.GetDamage());
            }
        }
    }

    private void TakeDamage(float damage)
    {
        if (_invulnerability) return;

        _invulnerability = true; // СРАЗУ устанавливаем флаг
        _health.TakeDamage(damage);

        PlayerGetDamage?.Invoke(_timeInvulnerability);
        StartCoroutine(InvulnerabilityCooldown());
    }

    private IEnumerator InvulnerabilityCooldown()
    {
        yield return new WaitForSeconds(_timeInvulnerability);
        _invulnerability = false; // Сбрасываем через время
    }
}