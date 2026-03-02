using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemPrefab : MonoBehaviour
{
    public Vector2 _startPosition;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private float _offset;
    private float _height = 0.2f;
    private float _speed = 1;

    public event Action OnItemPickedUp;

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        MovementPrefab();
    }
    private void OnEnable()
    {
        Debug.Log("GivePos Item Prefab OnEnable");

    }
    private void Start()
    {
        _offset = Random.Range(0f,360f);
        Debug.Log("GivePos Item Prefab");
    }
    private void MovementPrefab()
    {
        float newY = Mathf.Sin((Time.time + _offset) * _speed) * _height;
        transform.position = new Vector3(_startPosition.x, _startPosition.y + newY);
    }
    private void CacheComponents()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }
}
