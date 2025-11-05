using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateMob : MobState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsRunningHash = Animator.StringToHash("isRoaming");
    private Animator _animator;
    private MobAI _context;

    public IdleStateMob(MobAI context, Animator animator)
    {
        _context = context;
        _animator = animator;
    }
    public void Enter()
    {
        Debug.Log("Моб вошел в состояние покоя");
    }

    public void Update(Vector2 moveDirection)
    {

    }

    public void Exit()
    {
        Debug.Log("Моб вышел из состояния покоя");
    }
}
