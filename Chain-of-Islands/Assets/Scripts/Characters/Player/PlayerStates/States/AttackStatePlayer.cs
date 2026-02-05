using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackStatePlayer : PlayerState
{
    private PlayerVisualStateMachine _context; // Ссылка на StateMachine для смены состояний
    private Animator _animator;
    private Animator _handAnimator;
    private WeaponItem _currentWepon;
    private int _currentBodyAttackHash;
    private int _currentHandAttackHash;


    public AttackStatePlayer(PlayerVisualStateMachine context, Animator animator, Animator handAnimator)
    {
        _context = context;
        _animator = animator;
        _handAnimator = handAnimator;
    }

    // Метод для установки анимаций текущего оружия
    public void SetWeaponAnimations(WeaponItem weapon)
    {
        if (weapon == null)
        {
            Debug.LogError("Cannot set null weapon!");
            return;
        }
        if (weapon == _currentWepon)
        {
            return;
        }

        _currentBodyAttackHash = Animator.StringToHash(weapon.bodyAttackConditionName);
        _currentHandAttackHash = Animator.StringToHash(weapon.handAttackConditionName);

        _currentWepon = weapon;
    }

    public void Enter()
    {
        _animator.SetBool(_currentBodyAttackHash, true);
        _handAnimator.SetBool(_currentHandAttackHash, true);
    }

    public void Update(Vector2 moveDirection)
    {
        CheckState(moveDirection);
    }

    public void Exit()
    {
        _animator.SetBool(_currentBodyAttackHash, false);
        _handAnimator.SetBool(_currentHandAttackHash, false);
    }

    private void CheckState(Vector2 moveDirection)
    {
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);

        // Проверяем по имени текущей анимации
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_currentWepon.bodyAttackAnimationName) &&  state.normalizedTime >= 1.0f)
        {
            ChooseNextState(moveDirection);
        }
    }

    private void ChooseNextState(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            _context.ChangeState(_context.Running);
        }
        else
        {
            _context.ChangeState(_context.Idle);
        }
    }

}
