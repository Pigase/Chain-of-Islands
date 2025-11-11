using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingStateMob : MobState
{
    // Хеш параметра аниматора (должен совпадать с именем в Animator Controller)
    private static readonly int IsRoamingHash = Animator.StringToHash("isRoaming");

    private float _maxRoamDis = 7;
    private float _minRoamDis = 2;
    private Vector3 _roamPosition;
    private Vector3 _startPosition;

    private Animator _animator;
    private MobAI _context;
    private NavMeshAgent _navMeshAgent;

    public RoamingStateMob(MobAI context, Animator animator, NavMeshAgent navMeshAgent, Vector3 startPosition)
    {
        _context = context;
        _animator = animator;
        _navMeshAgent = navMeshAgent;
        _startPosition = startPosition;
    }
    public void Enter()
    {
        _animator.SetBool(IsRoamingHash, true);
        _roamPosition = GetRoamPosition();
        _navMeshAgent.SetDestination(_roamPosition);
        Debug.Log("Моб вошел в состояние скитания");
    }

    public void Update(Vector3 currentPosition)
{
    // Проверяем расстояние до цели
    float distanceToTarget = Vector3.Distance(currentPosition, _roamPosition);

    // Используем НАПРАВЛЕНИЕ ДВИЖЕНИЯ, а не направление к цели
    Vector3 movementDirection = _navMeshAgent.velocity.normalized;
    _context.FlipSprite(new Vector2(movementDirection.x, movementDirection.y));

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

    public void Exit()
    {
        // ОСТАНАВЛИВАЕМ агента при выходе из состояния
        if (_navMeshAgent != null && _navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.isStopped = true;  // Немедленная остановка
            _navMeshAgent.ResetPath();       // Очищаем путь
        }

        Debug.Log("Моб вышел из состояния скитания");
    }

    private Vector3 GetRoamPosition()
    {
        return _startPosition + GetRandomDir() * UnityEngine.Random.Range(_minRoamDis, _maxRoamDis);
    }

    private Vector3 GetRandomDir()
    {
        // Random.Range(-1, 1) может вернуть 0, что даст нулевое направление
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        return new Vector3(randomDir.x, randomDir.y, 0);
    }
}
