using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Mover _mover;
    private MobState _currentState; // Текущее активное состояние

    public IdleStateMob Idle { get; private set; }
    public RoamingStateMob Running { get; private set; }

    private void Awake()
    {
        _mover = new Mover(gameObject);

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_animator == null)
            Debug.LogError("Animator component is missing!", this);

        // Инициализация состояний с передачей зависимостей
        Idle = new IdleStateMob(this, _animator);
        Running = new RoamingStateMob(this, _animator);

        // Начальное состояние - моб начинает в покое
        ChangeState(Idle);
    }

    // Главный метод для обновления логики состояний
    // Вызывается извне (из Mob класса) каждый кадр
    public void ChooseState(Vector2 moveDirection)
    {
        _currentState?.Update(moveDirection); // Делегируем логику текущему состоянию
    }

    // Метод смены состояния с защитой от рекурсии
    public void ChangeState(MobState newState)
    {
        if (_currentState == newState)
            return; // Защита от перехода в то же состояние

        _currentState?.Exit();     // Выход из текущего состояния
        _currentState = newState;  // Смена состояния
        _currentState?.Enter();    // Вход в новое состояние
    }
}
