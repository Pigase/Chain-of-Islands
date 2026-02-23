using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOpener : MonoBehaviour
{
    private BuildingStation _building;
    private InventarySystem _inventarySystem;
    private Inventory _playerInventory;
    private Dictionary<string, int> allItems = new Dictionary<string, int>();

    public event Func<BuildingStation> OnRequestedBuilding;
    public event Action OnOpenedBuilding;

    private void Start()
    {
        _inventarySystem = GameManager.GetSystem<InventarySystem>();

        _playerInventory = _inventarySystem.Inventory;
    }
    public void OpenBuilding()
    {
        _building = OnRequestedBuilding.Invoke();

        if(IsCanOpenBuilding())
        {
            _building.BuildingOpen();

            foreach (var ingredient in _building.buildingStation.ingredients)
            {
                _inventarySystem.SubtractItems(ingredient.itemId, ingredient.amount);
            }

            OnOpenedBuilding?.Invoke();
        }
    }

    private bool IsCanOpenBuilding()
    {
        allItems.Clear();

        int countRecipeItem = 0;

        for (int i = 0; i < _playerInventory.Slots.Count; i++)
        {
            var slot = _playerInventory.Slots[i];

            if (slot.empty) continue;

            if (allItems.ContainsKey(_playerInventory.Slots[i].itemId))
            {
                allItems[_playerInventory.Slots[i].itemId] += _playerInventory.Slots[i].itemCount;
            }
            if (!allItems.ContainsKey(_playerInventory.Slots[i].itemId))
            {
                allItems.Add(_playerInventory.Slots[i].itemId, _playerInventory.Slots[i].itemCount);
            }
        }

        for (int i = 0; i < _building.buildingStation.ingredients.Count; i++)
        {
            ItemStack currentIngredient = _building.buildingStation.ingredients[i];

            if (!allItems.ContainsKey(currentIngredient.itemId) || allItems[currentIngredient.itemId] < currentIngredient.amount)
                continue;

            countRecipeItem++;
        }

        if (countRecipeItem >= _building.buildingStation.ingredients.Count)
            return true;

        return false;
    }

}
