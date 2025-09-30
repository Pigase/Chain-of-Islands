using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick _joystick; // ������ �� ��������� ���������
    [SerializeField] private float _speedPlayer;

    private Vector2 _moveDirectionPlayer; // ����������� �������� �� ���������
    private Mover _mover; // ��������� ����� ��� �����������

    private void Awake()
    {
        // ������� ��������� Mover, ��������� ������� GameObject
        _mover = new Mover(gameObject);
    }

    private void Update()
    {
        // �������� ����������� �������� �� ���������
        _moveDirectionPlayer = _joystick.FindingDirection();

        // �������� ����������� � Mover ��� ����������� �������
        _mover.MoveObject(_speedPlayer, _moveDirectionPlayer);
    }
}