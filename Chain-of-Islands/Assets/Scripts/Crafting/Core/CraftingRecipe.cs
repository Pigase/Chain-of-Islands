using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Craft" , fileName ="Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [Header("BASIC INFO")]
    [Tooltip("Уникальный ID рецепта (должен быть уникальным)")]
    public string recipeId;

    [Tooltip("Название рецепта")]
    public string recipeName;

    [Header("Ингредиенты")]
    [Tooltip("Что нужно для крафта")]
    public List<ItemStack> ingredients = new List<ItemStack>();

    [Header("Результат")]
    [Tooltip("Что получаем в результате крафта")]
    public ItemStack result;

    [Header("Требования")]
    [Tooltip("Какая станция нужна для крафта")]
    public Station requiredStation;

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(recipeId)) return false;
        if (ingredients == null || ingredients.Count == 0) return false;
        if (result == null || string.IsNullOrEmpty(result.itemId)) return false;
        if (result.amount <= 0) return false;

        foreach (var ingredient in ingredients)
        {
            if (string.IsNullOrEmpty(ingredient.itemId)) return false;
            if (ingredient.amount < 0) return false;
        }

        return true;
    }
}
