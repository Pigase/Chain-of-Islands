using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover
{
    private GameObject _ObjectToMove; // Ссылка на GameObject для перемещения

    // Конструктор - сохраняем ссылку на объект для перемещения
    public Mover(GameObject _gameObject)
    {
        _ObjectToMove = _gameObject;
    }

    // Основной метод перемещения объекта
    public void MoveObject(float _speedMoving, Vector2 _moveDirection)
    {
        // Проверяем, что направление не нулевое
        if (_moveDirection != Vector2.zero)
        {
            // Перемещаем объект в направлении _moveDirection со скоростью _speedMoving
            _ObjectToMove.transform.position += (Vector3)_moveDirection * _speedMoving * Time.deltaTime;
        }
    }
}