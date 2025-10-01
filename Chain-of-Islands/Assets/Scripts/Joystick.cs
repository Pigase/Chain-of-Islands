using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Joystick : MonoBehaviour
{
    [SerializeField] private GameObject _bigJoystick;
    [SerializeField] private GameObject _smallJoystick;

    [SerializeField] private float _radius = 1f; // ������ �������� ��������
    [SerializeField] private float _joystickZoneWidth = 3f; // ������ ���� �������� ������ ���������

    private Vector2 _directionToMove;
    private Vector2 _bigJoystickPosition;

    private void Awake()
    {
        _bigJoystick.SetActive(false);
    }

    public Vector2 FindingDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���������, ��� ������� � ����� ������ ���� ������
            if (IsInJoystickZone(touchPosition))
            {
                _bigJoystickPosition = touchPosition;
                _bigJoystick.transform.position = _bigJoystickPosition;
                _bigJoystick.SetActive(true);
            }
        }

        if (Input.GetMouseButton(0) && _bigJoystick.activeSelf)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ������ �� ������ �������� ��������� � ������� ����
            Vector2 directionVector = mouseWorldPos - (Vector2)_bigJoystick.transform.position;

            // ���������� - ����� ������� �����������
            float hypotenuse = directionVector.magnitude;

            if (hypotenuse > _radius)
            {
                // ���� ����� �� ������ - ������������ �������
                Vector2 limitedPos = (Vector2)_bigJoystick.transform.position + directionVector.normalized * _radius;
                _smallJoystick.transform.position = limitedPos;
                _directionToMove = directionVector.normalized; // ��������������� ������ �����������
            }
            else
            {
                // � �������� ������� - ������ ��������� �������� � ������� ����
                _smallJoystick.transform.position = mouseWorldPos;
                _directionToMove = directionVector / _radius; // ������ ����������� �� -1 �� 1
            }
        }
        else
        {
            _directionToMove.x = 0;
            _directionToMove.y = 0;
            _bigJoystick.SetActive(false);
        }

        return _directionToMove;
    }

    // ��������, ��������� �� ������� � ���� ��������� (����� ������ ����)
    private bool IsInJoystickZone(Vector2 worldPosition)
    {
        // �������� ������� ������ � ������� �����������
        Camera cam = Camera.main;
        float screenBottom = cam.transform.position.y - cam.orthographicSize;
        float screenLeft = cam.transform.position.x - cam.orthographicSize * cam.aspect;

        // ���� ��������� - ����� ������ �������
        float zoneRight = screenLeft + _joystickZoneWidth;
        float zoneTop = screenBottom + _joystickZoneWidth;

        return worldPosition.x >= screenLeft && worldPosition.x <= zoneRight &&
               worldPosition.y >= screenBottom && worldPosition.y <= zoneTop;
    }
}