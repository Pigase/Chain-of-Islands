using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CraftingSystem : MonoBehaviour
{
    [Header("Все рецепты крафтов")]
    [SerializeField] private List<CraftingRecipe> allRecipes;

    private Dictionary<string, CraftingRecipe> recipeMap;
    private ItemDataBase itemDataBase;

    public void Initialize(ItemDataBase db)
    {
        itemDataBase = db;
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

    public bool CanCraftRecipe(string recipeId, Dictionary<string,int> playerInventary)
    {
        if (!recipeMap.ContainsKey(recipeId))
            return false;

        CraftingRecipe recipe = recipeMap[recipeId];

        foreach (var ingredient in recipe.ingredients)
        {
            if (!playerInventary.ContainsKey(ingredient.itemId) || playerInventary[ingredient.itemId] < ingredient.amount) 
                return false;
        }
        
        return true;
    }

    public bool CraftItem(string recipeId, Dictionary<string,int> playerInventary)
    {
        if (!CanCraftRecipe(recipeId, playerInventary))
        {
            Debug.Log($"Не удалось скрафтить {recipeId}: не хватает материалов");
            return false;
        }

        CraftingRecipe recipe = recipeMap[recipeId];

        foreach (var ingredient in recipe.ingredients)
        {
            playerInventary[ingredient.itemId] -= ingredient.amount;

            if (playerInventary[ingredient.itemId] <= 0 )
                playerInventary.Remove(ingredient.itemId);      //не забудь переделать
        }

        if (playerInventary.ContainsKey(recipe.result.itemId))
        {
            playerInventary[recipe.result.itemId] += recipe.result.amount;
        }
        else
        {
            playerInventary.Add(recipe.result.itemId, recipe.result.amount);
        }
        Debug.Log($"Скрафтен: {recipe.result.itemId} x{recipe.result.amount}");

        return true;
    }

    public List<CraftingRecipe> GetRecipesForStation(CraftingStation station)
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
