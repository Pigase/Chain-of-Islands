using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualStateMachine : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerState _currentState; // ������� �������� ���������

    // ��������� - �������� ��� ����������� ������� �����
    public IdleState Idle { get; private set; }
    public RunningState Running { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_animator == null)
            Debug.LogError("Animator component is missing!", this);

        // ������������� ��������� � ��������� ������������
        Idle = new IdleState(this, _animator);
        Running = new RunningState(this, _animator);

        // ��������� ��������� - �������� �������� � �����
        ChangeState(Idle);
    }

    // ������� ����� ��� ���������� ������ ���������
    // ���������� ����� (�� Player ������) ������ ����
    public void ChooseState(Vector2 moveDirection)
    {
        _currentState?.Update(moveDirection); // ���������� ������ �������� ���������
    }

    // ����� ����� ��������� � ������� �� ��������
    public void ChangeState(PlayerState newState)
    {
        if (_currentState == newState)
            return; // ������ �� �������� � �� �� ���������

        _currentState?.Exit();     // ����� �� �������� ���������
        _currentState = newState;  // ����� ���������
        _currentState?.Enter();    // ���� � ����� ���������
    }

    // ���������� ����� - ������� ������� � ����������� �� �����������
    public void FlipSprite(Vector2 moveDirection)
    {
        if (moveDirection.x != 0)
        {
            _spriteRenderer.flipX = moveDirection.x < 0; // true = ������� �����
        }
    }
}