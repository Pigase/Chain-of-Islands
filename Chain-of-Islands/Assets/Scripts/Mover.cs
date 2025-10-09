using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover
{
    private GameObject _targetObject; // ������ �� GameObject ��� �����������

    // ����������� - ��������� ������ �� ������ ��� �����������
    public Mover(GameObject _targetGameObject)
    {
        _targetObject = _targetGameObject;
    }
        
    // �������� ����� ����������� �������
    public void MoveObjectInDerection(float _speedMoving, Vector2 _moveDirection)
    {
        // ���������, ��� ����������� �� �������
        if (_moveDirection != Vector2.zero)
        {
            // ���������� ������ � ����������� _moveDirection �� ��������� _speedMoving
            _targetObject.transform.position += (Vector3)_moveDirection * _speedMoving * Time.deltaTime;
        }
    }

    public void MoveObjectToPoint(float _speedMoving, Vector2 _pointToMove)
    {

            _targetObject.transform.position = Vector3.MoveTowards(_targetObject.transform.position, (Vector3)_pointToMove, _speedMoving * Time.deltaTime);

    }
}