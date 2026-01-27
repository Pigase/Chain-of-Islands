using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;

    public CraftingRecipe recipe { get; set; }

    private ItemDataBase _itemData;
    private bool _isInitialized = false;

    public event Action<CraftingRecipe> OnClicedOnCraft;
    
    private void Start()
    {
        _itemData = GameManager.GetSystem<ItemDataBase>();
        _isInitialized = true;

        if (_isInitialized)
        {
            if (recipe == null)
                throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");

            var itemInfo = _itemData.GetItem(recipe.result.itemId);

            _icon.sprite = itemInfo.icon;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(recipe);
        OnClicedOnCraft?.Invoke(recipe);
    }
}
