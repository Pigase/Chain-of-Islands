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
    private Dictionary<string,int> allItems = new Dictionary<string, int>();

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
        allItems.Clear();

        int countRecipeItem = 0;
        
        if (!recipeMap.ContainsKey(recipeId))
            return false;

        for(int i = 0; i < playerInventory.Slots.Count; i++)
        {
            var slot = playerInventory.Slots[i];

            if (slot.empty) continue;

            if (allItems.ContainsKey(playerInventory.Slots[i].itemId))
            {
                allItems[playerInventory.Slots[i].itemId] += playerInventory.Slots[i].itemCount;
            }
            if (!allItems.ContainsKey(playerInventory.Slots[i].itemId))
            {
                allItems.Add(playerInventory.Slots[i].itemId, playerInventory.Slots[i].itemCount);
            }
        }
        CraftingRecipe recipe = recipeMap[recipeId];

        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            ItemStack currentIngredient = recipe.ingredients[i];

            if (!allItems.ContainsKey(currentIngredient.itemId) || allItems[currentIngredient.itemId] < currentIngredient.amount)
                continue;

            countRecipeItem++;
        }

        if(countRecipeItem >= recipe.ingredients.Count)
            return true;

        return false;
    }

    public void CraftItem(string recipeId)
    {
        if (!CanCraftRecipe(recipeId))
        {
            Debug.Log($"Не удалось скрафтить {recipeId}: не хватает материалов");
            return;
        }

        CraftingRecipe recipe = recipeMap[recipeId];

        foreach (var ingredient in recipe.ingredients)
        {
            OnCraftedItemSubtract?.Invoke(ingredient.itemId,ingredient.amount);
        }

        OnCraftedItemAdded?.Invoke(recipe.result.itemId,recipe.result.amount);

        Debug.Log($"Скрафтен: {recipe.result.itemId} x{recipe.result.amount}");

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
