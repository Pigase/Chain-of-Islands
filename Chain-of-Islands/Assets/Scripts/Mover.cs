using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover
{
    private GameObject _targetObject; // Ссылка на GameObject для перемещения

    // Конструктор - сохраняем ссылку на объект для перемещения
    public Mover(GameObject _targetGameObject)
    {
        _targetObject = _targetGameObject;
    }
        
    // Основной метод перемещения объекта
    public void MoveObjectInDerection(float _speedMoving, Vector2 _moveDirection)
    {
        // Проверяем, что направление не нулевое
        if (_moveDirection != Vector2.zero)
        {
            // Перемещаем объект в направлении _moveDirection со скоростью _speedMoving
            _targetObject.transform.position += (Vector3)_moveDirection * _speedMoving * Time.deltaTime;
        }
    }

    public void MoveObjectToPoint(float _speedMoving, Vector2 _pointToMove)
    {

            _targetObject.transform.position = Vector3.MoveTowards(_targetObject.transform.position, (Vector3)_pointToMove, _speedMoving * Time.deltaTime);

    }
}