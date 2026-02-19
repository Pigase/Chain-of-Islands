using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadStatePlayer : PlayerState
{
    // Хеш параметра аниматора для оптимизации (быстрее чем строки)
    private static readonly int IsDeadHash = Animator.StringToHash("isDead");
    private static readonly int IsDeadHandHash = Animator.StringToHash("isDeadHand");
    private PlayerVisualStateMachine _context; // Ссылка на StateMachine для смены состояний
    private Animator _animator;
    private Animator _handAnimator;
    private HealthComponent _healthComponent;

    public DeadStatePlayer(PlayerVisualStateMachine context, Animator animator, Animator handAnimator , HealthComponent healthComponent)
    {
        _context = context;
        _animator = animator;
        _handAnimator = handAnimator;
        _healthComponent = healthComponent;
    }

    public void Enter()
    {
        _animator.SetBool(IsDeadHash, true); // Выключаем анимацию смерти
        _handAnimator.SetBool(IsDeadHandHash, true); // Выключаем анимацию смерти
    }

    public void Update(Vector2 moveDirection)
    {
        // Логика покоя
        // Проверяем жив ли
        if (_healthComponent.IsAlive) // Если есть жив
        {
            _context.ChangeState(_context.Idle); // Переходим в состояние покоя
        }
    }

    public void Exit()
    {
        _animator.SetBool(IsDeadHash, false);
        _handAnimator.SetBool(IsDeadHandHash, false); // Выключаем анимацию смерти
    }
}
