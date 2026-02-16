using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Mob;

public class MobAI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MobConfig _config;
    [SerializeField] private Mob _mob;

    private Vector3 _startPosition;

    private NavMeshAgent _navMeshAgent;
    private MobState _currentState;

    public IdleStateMob Idle { get; private set; }
    public RoamingStateMob Roaming { get; private set; }
    public ChasingStateMob Chasing { get; private set; }
    public AtackingStateMob Atacking { get; private set; }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        if (_animator == null)
            Debug.LogError("Animator component is missing!", this);
    }

    private void Start()
    {
        _startPosition = transform.position; // Уже установлена из пула!

        // Инициализация состояний с передачей зависимостей
        Idle = new IdleStateMob(this, _animator, _config);
        Chasing = new ChasingStateMob(this, _animator, _navMeshAgent, _config);
        Atacking = new AtackingStateMob(this, _animator, _config);
        Roaming = new RoamingStateMob(this, _animator, _navMeshAgent, _startPosition, _config);

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
        if (moveDirection.x != 0)
        {
            // Переворачиваем весь объект по оси X
            transform.localScale = new Vector3(
                moveDirection.x < 0 ? -1 : 1,  // X scale
                transform.localScale.y,         // Y scale оставляем как есть
                transform.localScale.z          // Z scale оставляем как есть
            );
        }
    }
}
