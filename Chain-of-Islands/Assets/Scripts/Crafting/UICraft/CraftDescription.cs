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
    [SerializeField] private RectTransform _content;
    [SerializeField] private TextMeshProUGUI _descriptionResult;
    [SerializeField] private IngridientSlot _prefabIngridientSlot;
    [SerializeField] private Image _iconResult;
    [SerializeField] private CreateCraftSlots _createCraftSlots;

    private int _poolCount = 20;
    private bool _autoExpande = true;
    private PoolMono<IngridientSlot> _pool;
    private int _lastIconCount;
    private ItemDataBase _itemData;
    private List<IngridientSlot> _iconsByIngridients;

    private void Start()
    {
        _itemData = GameManager.GetSystem<ItemDataBase>();
        _iconsByIngridients = new List<IngridientSlot>();

        _pool = new PoolMono<IngridientSlot>(_prefabIngridientSlot, _poolCount, transform);
        _pool.autoExpand = _autoExpande;
    }

    private void DataPurpose(CraftingRecipe recipe)
    {
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");

        Debug.Log(recipe + "CraftDescription");
        var recipeIngridient = recipe.ingredients.Count;

        for (int i = 0; i < _iconsByIngridients.Count; i++)
        {
            _iconsByIngridients[i].gameObject.SetActive(false);
        }

        _iconsByIngridients.Clear();

        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            var ingridientInfo = _itemData.GetItem(recipe.ingredients[i].itemId);

            var ingridient = _pool.GetFreeElement();
            ingridient.transform.SetParent(_content);
            ingridient.transform.localScale = Vector3.one;
            ingridient.icon.sprite = ingridientInfo.icon;
            _iconsByIngridients.Add(ingridient);
        }

        var itemId = recipe.result.itemId;
        var itemInfo = _itemData.GetItem(itemId);

        _iconResult.sprite = itemInfo.icon;
        _descriptionResult.text = itemInfo.description;

        _lastIconCount = recipe.ingredients.Count;
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
