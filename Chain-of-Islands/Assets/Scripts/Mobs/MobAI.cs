using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Mob;

public class MobAI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private MobConfig _config;

    private Vector3 _startPosition;

    private NavMeshAgent _navMeshAgent;
    private MobState _currentState; // Текущее активное состояние

    public IdleStateMob Idle { get; private set; }
    public RoamingStateMob Roaming { get; private set; }
    public ChasingStateMob Chasing { get; private set; }

    private void Awake()
    {
        _startPosition = transform.position;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        if (_animator == null)
            Debug.LogError("Animator component is missing!", this);

        // Инициализация состояний с передачей зависимостей
        Idle = new IdleStateMob(this, _animator, _config);
        Roaming = new RoamingStateMob(this, _animator, _navMeshAgent, _startPosition, _config);
        Chasing = new ChasingStateMob(this, _animator, _navMeshAgent, _config);

        // Начальное состояние - моб начинает в покое
        ChangeState(Idle);
    }

    // Главный метод для обновления логики состояний
    // Вызывается извне (из Mob класса) каждый кадр
    public void ChooseState()
    {
        _currentState?.Update(transform.position); // Делегируем логику текущему состоянию
    }

    // Метод смены состояния с защитой от рекурсии
    public void ChangeState(MobState newState)
    {
        if (_currentState == newState)
            return; // Защита от перехода в то же состояние

        _currentState?.Exit();     // Выход из текущего состояния
        _currentState = newState;  // Смена состояния
        _currentState?.Enter();    // Вход в новое состояние
    }

    // Визуальный метод - поворот спрайта в зависимости от направления
    public void FlipSprite(Vector2 moveDirection)
    {
        if (_spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is missing on mob!", this);
            return;
        }

        if (moveDirection.x != 0)
        {
            _spriteRenderer.flipX = moveDirection.x < 0; // true = смотрит влево
        }
    }
}
