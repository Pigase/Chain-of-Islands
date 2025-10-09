using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    // ��� ��������� ��������� ��� ����������� (������� ��� ������)
    private static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    private PlayerVisualStateMachine _context; // ������ �� StateMachine ��� ����� ���������
    private Animator _animator;

    public IdleState(PlayerVisualStateMachine context, Animator animator)
    {
        _context = context;
        _animator = animator;
    }

    public void Enter()
    {
        _animator.SetBool(IsRunningHash, false); // ��������� �������� ����
        Debug.Log("����� � ��������� �����");
    }

    public void Update(Vector2 moveDirection)
    {
        // ������ �����
        // ��������� ������� �������� � ���
        if (moveDirection != Vector2.zero) // ���� ���� ��������
        {
            _context.ChangeState(_context.Running); // ��������� � ��������� ����
        }
    }

    public void Exit()
    {
        Debug.Log("����� �� ��������� �����");
    }
}