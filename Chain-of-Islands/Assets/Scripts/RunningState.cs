using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{
    // ��� ��������� ��������� (������ ��������� � ������ � Animator Controller)
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
        _animator.SetBool(IsRunningHash, true); // �������� �������� ����
        Debug.Log("����� � ��������� ����");
    }

    public void Update(Vector2 moveDirection)
    {
        _context.FlipSprite(moveDirection); // ������������ ������ � ����������� ��������

        // ��������� ������� �������� � �����
        if (moveDirection == Vector2.zero) // ���� �������� ������������
        {
            _context.ChangeState(_context.Idle); // ������������ � ��������� �����
        }
    }

    public void Exit()
    {
        Debug.Log("����� �� ��������� ����");
    }
}