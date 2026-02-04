using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtackingStateMob : MobState
{
    // ’еш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsAtackingHash = Animator.StringToHash("isAtacking");
    private static readonly int AtackingHash = Animator.StringToHash("Atacking");

    private readonly MobConfig _config;
    private readonly Animator _animator;

    private float _distanceToPlayer;
    private Vector3 _currentPosition;

    private MobAI _context;
    private GameObject _player;

    public AtackingStateMob(MobAI context, Animator animator, MobConfig config)
    {
        _context = context;
        _animator = animator;
        _config = config;
        _player = PlayerService.PlayerGameObject;
    }

    public void Enter()
    {
        _animator.SetBool(IsAtackingHash, true);
    }

    public void Update(Vector3 currentPosition)
    {
        _distanceToPlayer = PlayerService.FindDistanceToPlayer(currentPosition);
        _currentPosition = currentPosition;

        CheckState();
    }

    public void Exit()
    {
        _animator.SetBool(IsAtackingHash, false);
    }

    private void CheckDirection()
    {
        float xDifference = PlayerService.PlayerGameObject.transform.position.x - _currentPosition.x;
        _context.FlipSprite(new Vector2(xDifference,0));
    }

    private void CheckState()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            CheckDirection();

            _animator.Play(AtackingHash, 0, 0f); // ¬озвращаем к 0 времени

            if (_distanceToPlayer >= _config.atackDistance)
            {
                _context.ChangeState(_context.Chasing);
            }
        }
    }
}
