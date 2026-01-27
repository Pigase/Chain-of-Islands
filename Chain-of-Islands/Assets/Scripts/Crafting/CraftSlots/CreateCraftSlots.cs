using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateCraftSlots : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] private RectTransform _content;
    [SerializeField] private CraftSlot _slotPrefab;

    public event Action<CraftingRecipe> OnGetedRecip;
    public List<CraftSlot> Slot => _slot;

    private CraftingSystem _craftingSystem;
    private List<CraftingRecipe> _recipe;
    private List<CraftSlot> _slot;

    private void Start()
    {
        _craftingSystem = GameManager.GetSystem<CraftingSystem>();
        _slot = new List<CraftSlot>();
        _recipe = new List<CraftingRecipe>();
        SlotSelection();

        CreateSlots();
    }

    private void CreateSlots()
    {
        var countSlots = _recipe.Count;

        if (countSlots > 0)
        {
            for (int i = 0; i < countSlots; i++)
            {
                var slot = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
                slot.transform.SetParent(_content);
                slot.recipe = _recipe[i];
                Debug.Log(_recipe[i]);
                slot.OnClicedOnCraft += OnGetRecipe;
                _slot.Add(slot);
            }
        }
    }

    private void SlotSelection()
    {
        for(int i = 0; i < _craftingSystem.AllRecipes.Count; i++)
        {
            if (_craftingSystem.AllRecipes[i].requiredStation == _station)
                _recipe.Add(_craftingSystem.AllRecipes[i]);
        }
    }

    private void OnGetRecipe(CraftingRecipe recipe)
    {
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");
        Debug.Log(recipe + " CreateCraftSlot");
        OnGetedRecip?.Invoke(recipe);
    } 
}
