using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    private Inventory playerInventory;
    private InventarySystem _inventarySystem;

    [Header("Все рецепты крафтов")]
    [SerializeField] private List<CraftingRecipe> allRecipes;

    public List<CraftingRecipe> AllRecipes => allRecipes;

    private ItemDataBase itemDataBase;
    private Dictionary<string, CraftingRecipe> recipeMap;

    public event Action<string,int> OnCraftedItemAdded;
    public event Action<string,int> OnCraftedItemSubtract;

    public void Initialize(ItemDataBase db)
    {
        itemDataBase = db;
        _inventarySystem = GameManager.GetSystem<InventarySystem>();
        playerInventory = _inventarySystem.Inventory;
        InitializeCraftingSystem();
    }

    private bool ValidateRecipe(CraftingRecipe recipe)
    {
        if (!recipe.IsValid())
        {
            Debug.LogWarning($"Рецепт {recipe.name} имеет некорректную структуру");
            return false;
        }

        if (!itemDataBase.ItemExists(recipe.result.itemId))
        {
            Debug.LogError($"Рецепт {recipe.recipeId}: предмет '{recipe.result.itemId}' не существует в базе!");
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (!itemDataBase.ItemExists(ingredient.itemId))
            {
                Debug.LogError($"Рецепт {recipe.recipeId}: ингредиент '{ingredient.itemId}' не существует в базе!");
                return false;
            }
        }

        return true; 
    }
    private void InitializeCraftingSystem()
    {
        if (itemDataBase == null)
        {
            Debug.LogError("ItemDataBase не найден!");
            return;
        }

        recipeMap = new Dictionary<string, CraftingRecipe>();
        int invalidCount = 0;

        foreach (var recipe in allRecipes)
        {
            if (ValidateRecipe(recipe)) 
            {
                if (!recipeMap.ContainsKey(recipe.recipeId))
                {
                    recipeMap.Add(recipe.recipeId, recipe);
                }
                else
                {
                    Debug.LogError($"Дубликат ID рецепта: {recipe.recipeId}");
                    invalidCount++;
                }
            }
            else
            {
                invalidCount++;
            }
        }

        Debug.Log($"CraftingSystem: {recipeMap.Count} валидных рецептов, {invalidCount} ошибок");
    }

    public bool CanCraftRecipe(string recipeId)
    {
        int countRecipeItem = 0;

        if (!recipeMap.ContainsKey(recipeId))
            return false;

        CraftingRecipe recipe = recipeMap[recipeId];

        for(int i = 0;i < playerInventory.Slots.Count; i++)
        {
            foreach (var ingredient in recipe.ingredients)
            {
                if (playerInventory.Slots[i].itemId == ingredient.itemId && playerInventory.Slots[i].itemCount >= ingredient.amount)
                    countRecipeItem++;
            }
        }

        if(countRecipeItem >= recipe.ingredients.Count)
            return true;

        return false;
    }

    public bool CraftItem(string recipeId)
    {
        if (!CanCraftRecipe(recipeId))
        {
            Debug.Log($"Не удалось скрафтить {recipeId}: не хватает материалов");
            return false;
        }

        CraftingRecipe recipe = recipeMap[recipeId];

        foreach (var ingredient in recipe.ingredients)
        {
            OnCraftedItemSubtract?.Invoke(ingredient.itemId,ingredient.amount);
        }

        OnCraftedItemAdded?.Invoke(recipe.result.itemId,recipe.result.amount);

        Debug.Log($"Скрафтен: {recipe.result.itemId} x{recipe.result.amount}");

        return true;
    }

    public List<CraftingRecipe> GetRecipesForStation(Station station)
    {
        List<CraftingRecipe> recipes = new List<CraftingRecipe>();

        foreach (var recipe in allRecipes)
        {
            if(recipe.requiredStation == station)
                recipes.Add(recipe);
        }
        return recipes;
    }
}
