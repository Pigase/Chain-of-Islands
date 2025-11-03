using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualStateMachine : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerState _currentState; // Текущее активное состояние

    // Состояния - свойства для безопасного доступа извне
    public IdleStatePlayer Idle { get; private set; }
    public RunningStatePlayer Running { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_animator == null)
            Debug.LogError("Animator component is missing!", this);

        // Инициализация состояний с передачей зависимостей
        Idle = new IdleStatePlayer(this, _animator);
        Running = new RunningStatePlayer(this, _animator);

        // Начальное состояние - персонаж начинает в покое
        ChangeState(Idle);
    }

    // Главный метод для обновления логики состояний
    // Вызывается извне (из Player класса) каждый кадр
    public void ChooseState(Vector2 moveDirection)
    {
        _currentState?.Update(moveDirection); // Делегируем логику текущему состоянию
    }

    // Метод смены состояния с защитой от рекурсии
    public void ChangeState(PlayerState newState)
    {
        if (_currentState == newState)
            return; // Защита от перехода в то же состояние

        _currentState?.Exit();     // Выход из текущего состояния
        _currentState = newState;  // Смена состояния
        _currentState?.Enter();    // Вход в новое состояние
    }

    // Визуальный метод - поворот спрайта в зависимости от направления
    public void FlipSprite(Vector2 moveDirection)
    {
        if (moveDirection.x != 0)
        {
            _spriteRenderer.flipX = moveDirection.x < 0; // true = смотрит влево
        }
    }
}