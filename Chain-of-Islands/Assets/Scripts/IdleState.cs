using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public void Enter()
    {
        Debug.Log("Вошел в состояние покоя");
        // Включить анимацию idle
    }

    public void Update()
    {
        // Логика покоя
    }

    public void Exit()
    {
        Debug.Log("Вышел из состояния покоя");
    }
}