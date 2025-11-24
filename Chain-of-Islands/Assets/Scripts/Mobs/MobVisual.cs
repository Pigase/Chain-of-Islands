using UnityEngine;

public class MobVisual : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject; // Перетащи сюда объект с двумя коллайдерами
    private PolygonCollider2D _attackCollider1;
    private PolygonCollider2D _attackCollider2;

    private void Awake()
    {
        // Получаем ВСЕ коллайдеры с целевого объекта
        PolygonCollider2D[] colliders = _targetObject.GetComponents<PolygonCollider2D>();

        _attackCollider1 = colliders[0]; // Первый коллайдер
        _attackCollider2 = colliders[1]; // Второй коллайдер
    }

    public void PolygonCollider1On() => _attackCollider1.enabled = true;
    public void PolygonCollider1Off() => _attackCollider1.enabled = false;
    public void PolygonCollider2On() => _attackCollider2.enabled = true;
    public void PolygonCollider2Off() => _attackCollider2.enabled = false;
}