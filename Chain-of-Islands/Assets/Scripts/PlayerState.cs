using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Интерфейс состояния - контракт для всех состояний
// Каждое состояние должно реализовать эти 3 метода
public interface PlayerState
{
    void Enter();  // Вызывается при входе в состояние
    void Update(Vector2 moveDirection); // Вызывается каждый кадр пока состояние активно
    void Exit();   // Вызывается при выходе из состояния
}