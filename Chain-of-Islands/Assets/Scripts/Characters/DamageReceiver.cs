using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private string[] _acceptedTypeNames = {"Sword"}; // "Sword", "Axe", "Pickaxe"
    [SerializeField] private int _tier = 0;

    public void TakeDamage(float damage, Item item)
    {
        if (item == null)//т.к. моб не имеет предметов он бьет и так
        {
            _health.TakeDamage(damage);
            return;
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

            Debug.Log($"Correct tool used: {item.itemName}, damage: {damage}");
            _health.TakeDamage(damage);
        }

        else
        {
            Debug.Log($"Wrong tool type! Accepted Type Names: {_acceptedTypeNames}, Got: {item.GetType().Name}");
        }
    }
}
