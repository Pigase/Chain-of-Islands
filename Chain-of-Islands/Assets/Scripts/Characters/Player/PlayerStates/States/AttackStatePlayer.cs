using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackStatePlayer : PlayerState
{
    // Хеш параметра аниматора для оптимизации (быстрее чем строки)
    private static readonly int IsSwordAttackHash = Animator.StringToHash("isSwordAttack");
    private static readonly int IsAttackStownSword = Animator.StringToHash("isAttackStownSword");
    private PlayerVisualStateMachine _context; // Ссылка на StateMachine для смены состояний
    private Animator _animator;
    private Animator _handAnimator;


    public AttackStatePlayer(PlayerVisualStateMachine context, Animator animator, Animator handAnimator)
    {
        _context = context;
        _animator = animator;
        _handAnimator = handAnimator;
    }

    public void Enter()
    {
        Debug.Log("Sword Attack");
        _animator.SetBool(IsSwordAttackHash, true); 
        _handAnimator.SetBool(IsAttackStownSword, true); 
    }

    public void Update(Vector2 moveDirection)
    {
        CheckState(moveDirection);
    }

    public void Exit()
    {
        _animator.SetBool(IsSwordAttackHash, false);
        _handAnimator.SetBool(IsAttackStownSword, false);
    }

    private void CheckState(Vector2 moveDirection)
    {
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("SwordAttack") && state.normalizedTime >= 1.0f)
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
