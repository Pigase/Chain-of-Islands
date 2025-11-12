using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingStateMob : MobState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsChasingHash = Animator.StringToHash("isChasing");

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

        Debug.Log("Моб вошел в состояние погони");
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

        float distanceToPlayer = Vector3.Distance(currentPosition, _player.transform.position);

        Chase();
        CheckDirection();
        CheckState(distanceToPlayer);
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

        Debug.Log("Моб вышел из состояния погони");
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

    private void CheckState(float distanceToPlayer)
    {

        Debug.Log("Расстояние до игрока: " + distanceToPlayer);
        if (distanceToPlayer >= _config.chaseExitDistance)
        {
            _context.ChangeState(_context.Roaming);
        }
    }
}
