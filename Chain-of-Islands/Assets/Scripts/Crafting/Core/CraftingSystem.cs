using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CraftingSystem : MonoBehaviour
{
    [Header("��� ������� �������")]
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
            Debug.LogWarning($"������ {recipe.name} ����� ������������ ���������");
            return false;
        }

        if (!itemDataBase.ItemExists(recipe.result.itemId))
        {
            Debug.LogError($"������ {recipe.recipeId}: ������� '{recipe.result.itemId}' �� ���������� � ����!");
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (!itemDataBase.ItemExists(ingredient.itemId))
            {
                Debug.LogError($"������ {recipe.recipeId}: ���������� '{ingredient.itemId}' �� ���������� � ����!");
                return false;
            }
        }

        return true; 
    }
    private void InitializeCraftingSystem()
    {
        if (itemDataBase == null)
        {
            Debug.LogError("ItemDataBase �� ������!");
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
                    Debug.LogError($"�������� ID �������: {recipe.recipeId}");
                    invalidCount++;
                }
            }
            else
            {
                invalidCount++;
            }
        }

        Debug.Log($"CraftingSystem: {recipeMap.Count} �������� ��������, {invalidCount} ������");
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
            Debug.Log($"�� ������� ��������� {recipeId}: �� ������� ����������");
            return false;
        }

        CraftingRecipe recipe = recipeMap[recipeId];

        foreach (var ingredient in recipe.ingredients)
        {
            playerInventary[ingredient.itemId] -= ingredient.amount;

            if (playerInventary[ingredient.itemId] <= 0 )
                playerInventary.Remove(ingredient.itemId);      //�� ������ ����������
        }

        if (playerInventary.ContainsKey(recipe.result.itemId))
        {
            playerInventary[recipe.result.itemId] += recipe.result.amount;
        }
        else
        {
            playerInventary.Add(recipe.result.itemId, recipe.result.amount);
        }
        Debug.Log($"��������: {recipe.result.itemId} x{recipe.result.amount}");

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
