using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private DamageProcessor _damageProcessor;
    [SerializeField] private string[] _acceptedTypeNames = { "Sword" }; // "Sword", "Axe", "Pickaxe"
    [SerializeField] private int _tier = 0;
    [SerializeField] private bool _isPlayer = false;

    public void TakeDamage(float damage, Item item)
    {
        // ПРОЦЕССИНГ УРОНА
        float finalDamage = damage;
        if (_damageProcessor != null)
        {
            finalDamage = _damageProcessor.ProcessDamage(damage);
        }

        // Проверка: можно ли нанести урон?
        if (!CanDealDamage(item))
        {
            return;
        }

        // НАНОСИМ УРОН ТОЛЬКО ЗДЕСЬ (ОДИН РАЗ!)
        Debug.Log($"Dealing {finalDamage} damage to {(item == null ? "mob attack" : item.itemName)}");
        _health.TakeDamage(finalDamage);
    }

    private bool CanDealDamage(Item item)
    {
        // Если атакует моб (item == null)
        if (item == null)
        {
            if (!_isPlayer)
            {
                Debug.Log("Mob attacking mob? Skipping damage");
                return false; // Мобы не наносят урон друг другу (или настраивай под свою логику)
            }
            return true; // Моб атакует игрока - можно наносить урон
        }

        // Проверка типа оружия
        bool typeAccepted = false;
        foreach (var typeName in _acceptedTypeNames)
        {
            if (item.GetType().Name == typeName)
            {
                typeAccepted = true;
                break;
            }
        }

        if (!typeAccepted)
        {
            Debug.Log($"Wrong tool type! Accepted: {string.Join(", ", _acceptedTypeNames)}, Got: {item.GetType().Name}");
            return false;
        }

        // Проверка тира оружия
        if (item is ToolItem tool && _tier > tool.tier)
        {
            Debug.Log($"Tool tier too low! Required: {_tier}, Got: {tool.tier}");
            return false;
        }

        Debug.Log($"Correct tool used: {item.itemName}");
        return true;
    }
}