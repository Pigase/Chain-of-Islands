using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examination : MonoBehaviour
{
    [SerializeField] private string _itemId;
    [SerializeField] private string craftRecipeId;
    [SerializeField] private Dictionary<string, int> testInventory = new Dictionary<string, int>();

    [Tooltip("items ID")]
    [SerializeField] private List<ItemStack> playerInventary;

    private CraftingSystem craftingSystem;
    private ItemDataBase itemDataBase;

    private void Start()
    {
        craftingSystem = GameManager.GetSystem<CraftingSystem>();
        itemDataBase = GameManager.GetSystem<ItemDataBase>();
        for (int i = 0; i < playerInventary.Count; i++)
        {
            testInventory.Add(playerInventary[i].itemId, playerInventary[i].amount);
        }
    }
    public void Exam()
    {
        itemDataBase.TestFindItem(_itemId); // ItemDataBase

        bool canCraft = craftingSystem.CanCraftRecipe(craftRecipeId, testInventory);
        craftingSystem.CraftItem(craftRecipeId, testInventory);

        //if (canCraft)
        //{
        //    CraftingSystem.instance.CraftItem(craftRecipeId, testInventory);
        //}
    }
}
