using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseStatePlayer : PlayerState
{
    private PlayerVisualStateMachine _context; // Ссылка на StateMachine для смены состояний
    private Animator _animator;
    private Animator _handAnimator;
    private EquipmentItem _currentEquipment;
    private int _currentBodyUseEquipmentHash;
    private int _currentHandUseEquipmentHash;

    public UseStatePlayer(PlayerVisualStateMachine context, Animator animator, Animator handAnimator)
    {
        _context = context;
        _animator = animator;
        _handAnimator = handAnimator;
    }

    // Метод для установки анимаций текущего оружия
    public void SetEqipmentAnimations(EquipmentItem equipment)
    {
        if (equipment == null)
        {
            Debug.LogError("Cannot set null weapon!");
            return;
        }
        if (equipment == _currentEquipment)
        {
            return;
        }

        _currentBodyUseEquipmentHash = Animator.StringToHash(equipment.bodyUseEquipmentConditionName);
        _currentHandUseEquipmentHash = Animator.StringToHash(equipment.handAttackConditionName);

        _currentEquipment = equipment;
    }

    public void Enter()
    {
        _animator.SetBool(_currentBodyUseEquipmentHash, true);
        _handAnimator.SetBool(_currentHandUseEquipmentHash, true);
    }

    public void Update(Vector2 moveDirection)
    {
        CheckState(moveDirection);
    }

    public void Exit()
    {
        _animator.SetBool(_currentBodyUseEquipmentHash, false);
        _handAnimator.SetBool(_currentHandUseEquipmentHash, false);
    }

    private void CheckState(Vector2 moveDirection)
    {
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);

        // Проверяем по имени текущей анимации
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_currentEquipment.bodyUseEquipmentAnimationName) &&  state.normalizedTime >= 1.0f)
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
