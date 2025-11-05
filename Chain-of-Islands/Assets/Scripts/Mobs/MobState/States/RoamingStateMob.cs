using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingStateMob : MobState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsRunningHash = Animator.StringToHash("isRoaming");
    private Animator _animator;
    private MobAI _context;

    public RoamingStateMob(MobAI context, Animator animator)
    {
        _context = context;
        _animator = animator;
    }
    public void Enter()
    {
        Debug.Log("Моб вошел в состояние скитания");
    }

    public void Update(Vector2 moveDirection)
    {

    }

    public void Exit()
    {
        Debug.Log("Моб вышел из состояния скитания");
    }
}
