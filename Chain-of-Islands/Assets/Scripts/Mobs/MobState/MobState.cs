using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Интерфейс состояния - контракт для всех состояний
// Каждое состояние должно реализовать эти 3 метода
public interface MobState
{
    void Enter();  // Вызывается при входе в состояние
    void Update(Vector3 currentPosition); // Вызывается каждый кадр пока состояние активно
    void Exit();   // Вызывается при выходе из состояния
}