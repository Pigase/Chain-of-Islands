using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatingCollidersInAnimations : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject; // Перетащи сюда объект с двумя коллайдерами
    private PolygonCollider2D[] _attackColliders;


    private void Awake()
    {
        // Получаем ВСЕ коллайдеры с целевого объекта
        _attackColliders = _targetObject.GetComponents<PolygonCollider2D>();
    }

    public void PolygonColliderOn(int numbercollider)
    {
        if (_attackColliders[numbercollider] != null)
        {
            _attackColliders[numbercollider].enabled = true;
        }
    }

    public void PolygonColliderOff(int numbercollider)
    {
        if (_attackColliders[numbercollider] != null)
        {
            _attackColliders[numbercollider].enabled = false;
        }
    }
}
