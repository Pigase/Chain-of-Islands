using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Craft" , fileName ="Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [Header("BASIC INFO")]
    [Tooltip("���������� ID ������� (������ ���� ����������)")]
    public string recipeId;

    [Tooltip("�������� �������")]
    public string recipeName;

    [Header("�����������")]
    [Tooltip("��� ����� ��� ������")]
    public List<ItemStack> ingredients = new List<ItemStack>();

    [Header("���������")]
    [Tooltip("��� �������� � ���������� ������")]
    public ItemStack result;

    [Header("����������")]
    [Tooltip("����� ������� ����� ��� ������")]
    public CraftingStation requiredStation;

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(recipeId)) return false;
        if (ingredients == null || ingredients.Count == 0) return false;
        if (result == null || string.IsNullOrEmpty(result.itemId)) return false;
        if (result.amount <= 0) return false;

        foreach (var ingredient in ingredients)
        {
            if (string.IsNullOrEmpty(ingredient.itemId)) return false;
            if (ingredient.amount <= 0) return false;
        }

        return true;
    }
}
