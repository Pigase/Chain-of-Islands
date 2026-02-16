using Unity.VisualScripting;
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
        // ÏÐÎÖÅÑÑÈÍÃ ÓÐÎÍÀ - êëþ÷åâîé ìîìåíò!
        float finalDamage = damage;
        if (_damageProcessor != null)
        {
            finalDamage = _damageProcessor.ProcessDamage(damage);
        }

        if (item == null)//ò.ê. ìîá íå èìååò ïðåäìåòîâ îí áüåò è òàê
        {
            if (_isPlayer)
            {
                _health.TakeDamage(finalDamage);
                return;
            }

            Debug.Log("Item is null");
        }

        bool typeAccepted = false;

        foreach (var typeName in _acceptedTypeNames)
        {
            if (item.GetType().Name == typeName)
            {
                typeAccepted = true;
                break;
            }
        }

        if (typeAccepted)
        {
            if (item is ToolItem tool && _tier > tool.tier)
            {
                Debug.Log($"Tool tier too low! Required: {_tier}, Got: {tool.tier}");
                return;
            }

            Debug.Log($"Correct tool used: {item.itemName}, damage: {finalDamage}");
            _health.TakeDamage(finalDamage);
        }

        else
        {
            Debug.Log($"Wrong tool type! Accepted Type Names: {_acceptedTypeNames}, Got: {item.GetType().Name}");
        }
    }
}
