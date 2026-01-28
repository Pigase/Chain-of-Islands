using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStatePlayer : PlayerState
{
    // Хеш параметра аниматора для оптимизации (быстрее чем строки)
    private static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    private PlayerVisualStateMachine _context; // Ссылка на StateMachine для смены состояний
    private Animator _animator;

    public IdleStatePlayer(PlayerVisualStateMachine context, Animator animator)
    {
        _context = context;
        _animator = animator;
    }

    public void Enter()
    {
        _animator.SetBool(IsRunningHash, false); // Выключаем анимацию бега
    }

    public void Update(Vector2 moveDirection)
    {
        // Логика покоя
        // Проверяем условия перехода в бег
        if (moveDirection != Vector2.zero) // Если есть движение
        {
            _context.ChangeState(_context.Running); // Переходим в состояние бега
        }
    }

    public void Exit()
    {
    }
}