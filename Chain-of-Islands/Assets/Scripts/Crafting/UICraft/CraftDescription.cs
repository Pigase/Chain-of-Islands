using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftDescription : MonoBehaviour
{
    [Header("Slots Prefab")]
    [SerializeField] private IngridientSlot _prefabIngridientSlot;
    [Header("Content")]
    [SerializeField] private RectTransform _content;
    [Header("Text Result")]
    [SerializeField] private TextMeshProUGUI _descriptionResult;
    [Header("Icons Result")]
    [SerializeField] private List<Image> _iconResult;
    [Header("Other Component")]
    [SerializeField] private CreateCraftSlots _createCraftSlots;

    private int _poolCount = 20;
    private bool _autoExpande = true;
    private PoolMono<IngridientSlot> _pool;
    private int _lastIconCount;
    private ItemDataBase _itemData;
    private List<IngridientSlot> _iconsByIngridients;
    private int recipeIngridientCount;

    private void Start()
    {
        _itemData = GameManager.GetSystem<ItemDataBase>();
        _iconsByIngridients = new List<IngridientSlot>();

        _pool = new PoolMono<IngridientSlot>(_prefabIngridientSlot, _poolCount, _content.transform);
        _pool.autoExpand = _autoExpande;
    }

    private void DataPurpose(CraftingRecipe recipe)
    {
        if(recipe == null)
        {
            recipeIngridientCount = 0;
        }
        else
        {
            recipeIngridientCount = recipe.ingredients.Count;
        }

        ResetIngridients();

        IngridientsIconDisplay(recipeIngridientCount,recipe);

        ResultIconDisplay(_iconResult.Count, recipe);
    }

    private void ResultIconDisplay(int amount, CraftingRecipe recipe)
    {
        string itemId = recipe.result.itemId;
        Item itemInfo = _itemData.GetItem(itemId);

        for (int i = 0; i < amount; i++)
        {
            _iconResult[i].gameObject.SetActive(true);
            _iconResult[i].sprite = itemInfo.icon;
            _descriptionResult.text = itemInfo.description;
        }
    }

    private void IngridientsIconDisplay(int amountSlot, CraftingRecipe recipe)
    {
        for (int i = 0; i < amountSlot; i++)
        {
            Item ingridientInfo = _itemData.GetItem(recipe.ingredients[i].itemId);

            IngridientSlot ingridient = _pool.GetFreeElement();
            ingridient.transform.SetParent(_content);
            ingridient.transform.localScale = Vector3.one;
            ingridient.icon.sprite = ingridientInfo.icon;
            _iconsByIngridients.Add(ingridient);
        }
    }
    private void ResetIngridients()
    {
        for (int i = 0; i < _iconsByIngridients.Count; i++)
        {
            _iconsByIngridients[i].gameObject.SetActive(false);
        }

        _iconsByIngridients.Clear();
    }
    private void OnEnable()
    {
        _createCraftSlots.OnGetedRecip += DataPurpose;
    }

    private void OnDisable()
    {
        _createCraftSlots.OnGetedRecip -= DataPurpose;
    }
}
