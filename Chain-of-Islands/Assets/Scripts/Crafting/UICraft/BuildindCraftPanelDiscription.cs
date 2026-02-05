using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildindCraftPanelDiscription : MonoBehaviour
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
    [SerializeField] private StationIdentifier _stationIdentifier;

    private int _poolCount = 20;
    private bool _autoExpande = true;
    private PoolMono<IngridientSlot> _pool;
    private ItemDataBase _itemData;
    private List<IngridientSlot> _iconsByIngridients;
    private int _lastIconCount;

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

        int recipeIngridient = recipe.ingredients.Count;

        for (int i = 0; i < _iconsByIngridients.Count; i++)
        {
            _iconsByIngridients[i].gameObject.SetActive(false);
        }

        _iconsByIngridients.Clear();

        CreateSlot(_iconsByIngridients.Count, recipe);

        string itemId = recipe.result.itemId;
        Item itemInfo = _itemData.GetItem(itemId);

        for (int i = 0; i < _iconResult.Count; i++)
        {
            _iconResult[i].gameObject.SetActive(true);
            _iconResult[i].sprite = itemInfo.icon;
            _descriptionResult.text = itemInfo.description;

            _lastIconCount = recipe.ingredients.Count;
        }
    }

    private void CreateSlot(int amountSlot, CraftingRecipe recipe)
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

    private void OnEnable()
    {
        _createCraftSlots.OnGetedRecip += DataPurpose;
    }

    private void OnDisable()
    {
        _createCraftSlots.OnGetedRecip -= DataPurpose;
    }
}
