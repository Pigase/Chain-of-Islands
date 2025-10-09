using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� ��������� - �������� ��� ���� ���������
// ������ ��������� ������ ����������� ��� 3 ������
public interface PlayerState
{
    void Enter();  // ���������� ��� ����� � ���������
    void Update(Vector2 moveDirection); // ���������� ������ ���� ���� ��������� �������
    void Exit();   // ���������� ��� ������ �� ���������
}