using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStatePlayer : PlayerState
{
    // Хеш параметра аниматора для оптимизации (быстрее чем строки)
    private static readonly int IsIdleHash = Animator.StringToHash("isIdle");
    private PlayerVisualStateMachine _context; // Ссылка на StateMachine для смены состояний
    private Animator _animator;

    public IdleStatePlayer(PlayerVisualStateMachine context, Animator animator)
    {
        _context = context;
        _animator = animator;
    }

    public void Enter()
    {
        _animator.SetBool(IsIdleHash, true); // Выключаем анимацию бега
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
        _animator.SetBool(IsIdleHash, false);
    }
}