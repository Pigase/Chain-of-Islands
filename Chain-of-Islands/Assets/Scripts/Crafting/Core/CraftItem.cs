using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItem : MonoBehaviour
{
    [SerializeField] private CreateCraftSlots _createCraftSlots;

    private CraftingSystem _craftingSystem;
    private CraftingRecipe _craftRecipe; 

    private void Start()
    {
        _craftingSystem = GameManager.GetSystem<CraftingSystem>();
    }
    public void Craft()
    {
        if (_craftingSystem != null)
        {
            var recipeId = _craftRecipe.recipeId;

            _craftingSystem.CraftItem(recipeId);
        }
    }

    private void GetRecipe(CraftingRecipe recipe )
    {
        _craftRecipe = recipe;
    }
    private void OnEnable()
    {
        _createCraftSlots.OnGetedRecip += GetRecipe;
    }
    private void OnDisable()
    {
        _createCraftSlots.OnGetedRecip -= GetRecipe;
    }
}
