using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOpener : MonoBehaviour
{
    private BuildingStation _building;
    private InventarySystem _inventarySystem;
    private Inventory _playerInventory;

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
            _building.IsBuildingOpen = true;

            foreach (var ingredient in _building.buildingStation.ingredients)
            {
                _inventarySystem.SubtractItems(ingredient.itemId, ingredient.amount);
            }

            OnOpenedBuilding?.Invoke();
        }
    }

    private bool IsCanOpenBuilding()
    {
        int countRecipeItem = 0;

        for (int i = 0; i < _playerInventory.Slots.Count; i++)
        {
            foreach (var ingredient in _building.buildingStation.ingredients)
            {
                if (_playerInventory.Slots[i].itemId == ingredient.itemId && _playerInventory.Slots[i].itemCount >= ingredient.amount)
                    countRecipeItem++;
            }
        }

        if (countRecipeItem >= _building.buildingStation.ingredients.Count)
            return true;

        return false;
    }

}
