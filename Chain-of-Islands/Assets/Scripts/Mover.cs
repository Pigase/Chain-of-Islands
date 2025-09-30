using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover
{
    private GameObject _ObjectToMove; // ������ �� GameObject ��� �����������

    // ����������� - ��������� ������ �� ������ ��� �����������
    public Mover(GameObject _gameObject)
    {
        _ObjectToMove = _gameObject;
    }

    // �������� ����� ����������� �������
    public void MoveObject(float _speedMoving, Vector2 _moveDirection)
    {
        // ���������, ��� ����������� �� �������
        if (_moveDirection != Vector2.zero)
        {
            // ���������� ������ � ����������� _moveDirection �� ��������� _speedMoving
            _ObjectToMove.transform.position += (Vector3)_moveDirection * _speedMoving * Time.deltaTime;
        }
    }
}