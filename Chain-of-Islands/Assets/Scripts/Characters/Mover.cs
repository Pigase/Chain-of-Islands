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
    public void MoveObjectInDerection(float speedMoving, Vector2 moveDirection, Rigidbody2D rb)
    {
        if (moveDirection != Vector2.zero)
        {
            Vector2 newPosition = rb.position + moveDirection * speedMoving * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }

    public void MoveObjectToPoint(float _speedMoving, Vector2 _pointToMove)
    {

            _targetObject.transform.position = Vector3.MoveTowards(_targetObject.transform.position, (Vector3)_pointToMove, _speedMoving * Time.deltaTime);

    }
}