using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RoamingStateMob : MobState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsRoamingHash = Animator.StringToHash("isRoaming");

    private Vector3 _roamPosition;
    private Vector3 _startPosition;

    private float _distanceToPlayer;

    private readonly MobConfig _config;
    private readonly Animator _animator;

    private MobAI _context;
    private NavMeshAgent _navMeshAgent;
    private UnityEngine.GameObject _player;


    public RoamingStateMob(MobAI context, Animator animator, NavMeshAgent navMeshAgent, Vector3 startPosition, MobConfig config)
    {
        _context = context;
        _animator = animator;
        _navMeshAgent = navMeshAgent;
        _startPosition = startPosition;
        _config = config;
        _player = PlayerService.PlayerGameObject;
    }

    public void Enter()
    {
        _animator.SetBool(IsRoamingHash, true);
        _roamPosition = GetRoamPosition();
        _navMeshAgent.SetDestination(_roamPosition);
        _navMeshAgent.speed = _config.speedRoaming;
    }

    public void Update(Vector3 currentPosition)
    {
        _distanceToPlayer = PlayerService.FindDistanceToPlayer(currentPosition);

        // Проверяем расстояние до цели
        float distanceToTarget = Vector3.Distance(currentPosition, _roamPosition);

        CheckDirection();
        CheckState(distanceToTarget);
    }

    public void Exit()
    {
        // ОСТАНАВЛИВАЕМ агента при выходе из состояния
        if (_navMeshAgent != null && _navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.isStopped = true;  // Немедленная остановка
            _navMeshAgent.ResetPath();       // Очищаем путь
        }

        _animator.SetBool(IsRoamingHash, false);
    }

    private void CheckDirection()
    {
        // Используем НАПРАВЛЕНИЕ ДВИЖЕНИЯ, а не направление к цели
        Vector3 movementDirection = _navMeshAgent.velocity.normalized;
        _context.FlipSprite(new Vector2(movementDirection.x, movementDirection.y));
    }

    private void CheckState(float distanceToTarget)
    {
        // Проверить, нашли ли объект, и затем использовать
        if (_player != null)
        {
            //Debug.Log("Расстояние до игрока: " + distanceToPlayer);
            if (_distanceToPlayer < _config.chaseStartDistance)
            {
                _context.ChangeState(_context.Chasing);
            }
        }
        else
        {
            Debug.LogWarning("Объект с тегом 'Player' не найден!");
        }

        // Если близко к цели
        if (distanceToTarget < 0.5f)
        {
            _context.ChangeState(_context.Idle);
        }

        // Если агент застрял
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
        {
            _context.ChangeState(_context.Idle);
        }
    }

    private Vector3 GetRoamPosition()
    {
        return _startPosition + GetRandomDir() * UnityEngine.Random.Range(_config.minRoamDistance, _config.maxRoamDistance);
    }

    private Vector3 GetRandomDir()
    {
        // Random.Range(-1, 1) может вернуть 0, что даст нулевое направление
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        return new Vector3(randomDir.x, randomDir.y, 0);
    }

}
