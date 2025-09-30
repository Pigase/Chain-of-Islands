using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick _joystick; // Ссылка на компонент джойстика
    [SerializeField] private float _speedPlayer;

    private Vector2 _moveDirectionPlayer; // Направление движения от джойстика
    private Mover _mover; // Кастомный класс для перемещения

    private void Awake()
    {
        // Создаем экземпляр Mover, передавая текущий GameObject
        _mover = new Mover(gameObject);
    }

    private void Update()
    {
        // Получаем направление движения от джойстика
        _moveDirectionPlayer = _joystick.FindingDirection();

        // Передаем направление в Mover для перемещения объекта
        _mover.MoveObject(_speedPlayer, _moveDirectionPlayer);
    }
}