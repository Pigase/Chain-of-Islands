using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class IdleStateMob : MobState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsIdleHash = Animator.StringToHash("isIdle");

    private float _enterTime;

    private readonly MobConfig _config;
    private readonly Animator _animator;

    private MobAI _context;
    private GameObject _player;

    public IdleStateMob(MobAI context, Animator animator, MobConfig config)
    {
        _context = context;
        _animator = animator;
        _config = config;
        _player = PlayerService.PlayerGameObject;
    }

    public void Enter()
    {
        _animator.SetBool(IsIdleHash, true);
        _enterTime = Time.time; // Запоминаем время входа

        Debug.Log("Моб вошел в состояние покоя");
    }

    public void Update(Vector3 currentPosition)
    {
        float distanceToPlayer;

        // Проверить, нашли ли объект, и затем использовать
        if (_player == null)
        {
            Debug.LogWarning("Объект с тегом 'Player' не найден!");
            distanceToPlayer = Mathf.Infinity;
        }
        else
        {
            distanceToPlayer = Vector3.Distance(currentPosition, _player.transform.position);
        }

        CheckState(distanceToPlayer);
    }

    public void Exit()
    {
        _animator.SetBool(IsIdleHash, false);

        Debug.Log("Моб вышел из состояния покоя");
    }

    private void CheckState(float distanceToPlayer)
    {
        // Проверить, нашли ли объект, и затем использовать
        if (_player != null)
        {
            Debug.Log("Расстояние до игрока: " + distanceToPlayer);
            if (distanceToPlayer < _config.chaseStartDistance)
            {
                _context.ChangeState(_context.Chasing);
            }
        }
        else
        {
            Debug.LogWarning("Объект с тегом 'Player' не найден!");
        }

        // Проверяем, прошло ли достаточно времени
        if (Time.time - _enterTime >= _config.idleTime)
        {
            _context.ChangeState(_context.Roaming);
        }

    }
}
