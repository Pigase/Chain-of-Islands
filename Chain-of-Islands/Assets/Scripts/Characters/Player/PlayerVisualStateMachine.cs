using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualStateMachine : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _handAnimator;
    [SerializeField] private ItemUseHandler _itemUseHandler;
    [SerializeField] private HealthComponent _healthComponent;

    private Animator _animator;
    private PlayerState _currentState; // Текущее активное состояние
    public System.Action<bool> OnStateWithNoMoveChanged; // Событие изменения состояния атаки

    // Состояния - свойства для безопасного доступа извне
    public IdleStatePlayer Idle { get; private set; }
    public RunningStatePlayer Running { get; private set; }
    public UseStatePlayer Use { get; private set; }
    public DeadStatePlayer Death { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
            Debug.LogError("Animator component is missing!", this);

        // Инициализация состояний с передачей зависимостей
        Idle = new IdleStatePlayer(this, _animator, _handAnimator);
        Running = new RunningStatePlayer(this, _animator, _handAnimator);
        Use = new UseStatePlayer(this, _animator, _handAnimator);
        Death = new DeadStatePlayer(this, _animator, _handAnimator, _healthComponent);
        // Начальное состояние - персонаж начинает в покое
        ChangeState(Idle);
    }

    private void OnEnable()
    {
        _healthComponent.OnDeath += PlayerDied;
        _player.PlayerGetDamage += ShowDamage;
        _itemUseHandler.UseEquipment += PlayerUseEquipment;
    }

    private void OnDisable()
    {
        _healthComponent.OnDeath -= PlayerDied;
        _player.PlayerGetDamage -= ShowDamage;
        _itemUseHandler.UseEquipment -= PlayerUseEquipment;
    }

    // Главный метод для обновления логики состояний
    // Вызывается извне (из Player класса) каждый кадр
    public void ChooseState(Vector2 moveDirection)
    {
        _currentState?.Update(moveDirection); // Делегируем логику текущему состоянию
    }

    // Метод смены состояния с защитой от рекурсии
    public void ChangeState(PlayerState newState)
    {
        if (_currentState == newState)
            return; // Защита от перехода в то же состояние

        _currentState?.Exit();     // Выход из текущего состояния
        _currentState = newState;  // Смена состояния
        _currentState?.Enter();    // Вход в новое состояние

        // Оповещаем об изменении состояния атаки
        bool StateWithNoMove = (newState == Use);
        OnStateWithNoMoveChanged?.Invoke(StateWithNoMove);
    }

    // Визуальный метод - поворот спрайта в зависимости от направления
    public void FlipSprite(Vector2 moveDirection)
    {
        if (moveDirection.x != 0)
        {
            // Переворачиваем весь объект по оси X
            _player.transform.localScale = new Vector3(
                moveDirection.x < 0 ? -1 : 1,  // X scale
                transform.localScale.y,         // Y scale оставляем как есть
                transform.localScale.z          // Z scale оставляем как есть
            );
        }
    }

    private void ShowDamage(float timeInvulnerability)
    {

        StartCoroutine(Invulnerability(timeInvulnerability));
    }

    private void PlayerDied()
    {
        ChangeState(Death);
    }
    private void PlayerUseEquipment(EquipmentItem equipment)
    {
        Use.SetEqipmentAnimations(equipment);

       ChangeState(Use);
    }

    private IEnumerator Invulnerability(float secondsForShowDamge)
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(secondsForShowDamge);
        sprite.color = Color.white;
    }
}