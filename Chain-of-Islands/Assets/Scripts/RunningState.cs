using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    private PlayerVisualStateMachine _context;
    private Animator _animator;

    public RunningState(PlayerVisualStateMachine context, Animator animator)
    {
        _context = context;
        _animator = animator;
    }

    public void Enter()
    {
        _animator.SetBool(IsRunningHash, true); // Включаем анимацию бега
        Debug.Log("Вошел в состояние бега");
    }

    public void Update(Vector2 moveDirection)
    {
        _context.FlipSprite(moveDirection); // Поворачиваем спрайт в направлении движения

        // Проверяем условия перехода в покой
        if (moveDirection == Vector2.zero) // Если движение остановилось
        {
            _context.ChangeState(_context.Idle); // Возвращаемся в состояние покоя
        }
    }

    public void Exit()
    {
        Debug.Log("Вышел из состояния бега");
    }
}