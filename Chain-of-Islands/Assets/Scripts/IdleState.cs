using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public void Enter()
    {
        Debug.Log("����� � ��������� �����");
        // �������� �������� idle
    }

    public void Update()
    {
        // ������ �����
    }

    public void Exit()
    {
        Debug.Log("����� �� ��������� �����");
    }
}