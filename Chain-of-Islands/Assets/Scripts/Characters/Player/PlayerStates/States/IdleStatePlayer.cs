using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStatePlayer : PlayerState
{
    // Хеш параметра аниматора для оптимизации (быстрее чем строки)
    private static readonly int IsIdleHash = Animator.StringToHash("isIdle");
    private static readonly int IsIdleHandHash = Animator.StringToHash("isIdleHand");
    private PlayerVisualStateMachine _context; // Ссылка на StateMachine для смены состояний
    private Animator _animator;
    private Animator _handAnimator;


    public IdleStatePlayer(PlayerVisualStateMachine context, Animator animator, Animator handAnimator)
    {
        _context = context;
        _animator = animator;
        _handAnimator = handAnimator;

    }

    public void Enter()
    {
        _animator.SetBool(IsIdleHash, true); // Выключаем анимацию бега
        _handAnimator.SetBool(IsIdleHandHash, true); // Выключаем анимацию бега
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
        _handAnimator.SetBool(IsIdleHandHash, false); // Выключаем анимацию бега
    }
}