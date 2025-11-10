using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleStateMob : MobState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsRoamingHash = Animator.StringToHash("isRoaming");

    private float _timeToWait = 2f;
    private float _enterTime;

    private Animator _animator;
    private MobAI _context;

    public IdleStateMob(MobAI context, Animator animator)
    {
        _context = context;
        _animator = animator;
    }
    public void Enter()
    {
        _animator.SetBool(IsRoamingHash, false);
        _enterTime = Time.time; // Запоминаем время входа

        Debug.Log("Моб вошел в состояние покоя");
    }

    public void Update(Vector3 currentPosition)
    {
        // Проверяем, прошло ли достаточно времени
        if (Time.time - _enterTime >= _timeToWait)
        {
            _context.ChangeState(_context.Roaming);
        }
    }

    public void Exit()
    {
        Debug.Log("Моб вышел из состояния покоя");
    }
}
