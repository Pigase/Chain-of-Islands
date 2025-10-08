using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    private bool _wasRunning;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (_animator == null)
            Debug.LogError("Animator component is missing!", this);
    }

    public void IsRunning(Vector2 moveDirection)
    {
        bool isRunning = moveDirection != Vector2.zero;
        
        // Оптимизация: обновляем аниматор только при изменении состояния
        if (_wasRunning != isRunning)
        {
            _animator.SetBool(IsRunningHash, isRunning);
            _wasRunning = isRunning;
        }
        
        FlipSprite(moveDirection);
    }

    private void FlipSprite(Vector2 moveDirection)
    {
        if (moveDirection.x != 0)
        {
            _spriteRenderer.flipX = moveDirection.x < 0;
        }
    }
}