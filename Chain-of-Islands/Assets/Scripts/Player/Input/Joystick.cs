using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Joystick : MonoBehaviour
{
    [SerializeField] private GameObject _bigJoystick;
    [SerializeField] private GameObject _smallJoystick;

    [SerializeField] private float _radius = 1f; // радиус большого джостика
    [SerializeField] private float _joystickZoneWidth = 3f; // Ширина зоны действия спавна джойстика

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

            // Проверяем, что касание в левой нижней зоне экрана
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

            // Вектор от центра большого джойстика к позиции мыши
            Vector2 directionVector = mouseWorldPos - (Vector2)_bigJoystick.transform.position;

            // Гипотенуза - длина вектора направления
            float hypotenuse = directionVector.magnitude;

            if (hypotenuse > _radius)
            {
                // Если вышли за радиус - ограничиваем позицию
                Vector2 limitedPos = (Vector2)_bigJoystick.transform.position + directionVector.normalized * _radius;
                _smallJoystick.transform.position = limitedPos;
                _directionToMove = directionVector.normalized; // Нормализованный вектор направления
            }
            else
            {
                // В пределах радиуса - ставим маленький джойстик к позиции мыши
                _smallJoystick.transform.position = mouseWorldPos;
                _directionToMove = directionVector / _radius; // Вектор направления от -1 до 1
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

    // Проверка, находится ли позиция в зоне джойстика (левый нижний угол)
    private bool IsInJoystickZone(Vector2 worldPosition)
    {
        // Получаем границы камеры в мировых координатах
        Camera cam = Camera.main;
        float screenBottom = cam.transform.position.y - cam.orthographicSize;
        float screenLeft = cam.transform.position.x - cam.orthographicSize * cam.aspect;

        // Зона джойстика - левый нижний квадрат
        float zoneRight = screenLeft + _joystickZoneWidth;
        float zoneTop = screenBottom + _joystickZoneWidth;

        return worldPosition.x >= screenLeft && worldPosition.x <= zoneRight &&
               worldPosition.y >= screenBottom && worldPosition.y <= zoneTop;
    }
}