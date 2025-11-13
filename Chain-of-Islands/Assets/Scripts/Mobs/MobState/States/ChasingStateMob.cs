using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingStateMob : MobState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsChasingHash = Animator.StringToHash("isChasing");

    private float _distanceToPlayer;

    private readonly MobConfig _config;
    private readonly Animator _animator;

    private MobAI _context;
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;

    public ChasingStateMob(MobAI context, Animator animator, NavMeshAgent navMeshAgent, MobConfig config)
    {
        _context = context;
        _animator = animator;
        _navMeshAgent = navMeshAgent;
        _config = config;
        _player = PlayerService.PlayerGameObject;
    }

    public void Enter()
    {
        _animator.SetBool(IsChasingHash, true);
        _navMeshAgent.speed = _config.speedChasing;
    }

    public void Update(Vector3 currentPosition)
    {
        // Проверить, нашли ли объект, и затем использовать
        if (_player == null)
        {
            _context.ChangeState(_context.Idle);
            Debug.LogWarning("Объект с тегом 'Player' не найден!");
            return;
        }

        _distanceToPlayer = PlayerService.FindDistanceToPlayer(currentPosition);

        Chase();
        CheckDirection();
        CheckState();
    }

    public void Exit()
    {
        // ОСТАНАВЛИВАЕМ агента при выходе из состояния
        if (_navMeshAgent != null && _navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.isStopped = true;  // Немедленная остановка
            _navMeshAgent.ResetPath();       // Очищаем путь
        }

        _animator.SetBool(IsChasingHash, false);
    }

    private void Chase()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }

    private void CheckDirection()
    {
        // Используем НАПРАВЛЕНИЕ ДВИЖЕНИЯ, а не направление к цели
        Vector3 movementDirection = _navMeshAgent.velocity.normalized;
        _context.FlipSprite(new Vector2(movementDirection.x, movementDirection.y));
    }

    private void CheckState()
    {

        Debug.Log("Расстояние до игрока: " + _distanceToPlayer);
        if (_distanceToPlayer >= _config.chaseExitDistance)
        {
            _context.ChangeState(_context.Roaming);
        }
        if(_distanceToPlayer <= _config.atackDistance)
        {
            _context.ChangeState(_context.Atacking);
        }
    }
}
