using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateCraftSlots : MonoBehaviour
{
    [SerializeField] private int _poolCount = 20;
    [SerializeField] private bool _autoExpande = true;
    [SerializeField] private CraftSlot _slotPrefab;
    [SerializeField] private PlayerCraft _playerCraft;
    [SerializeField] private BuildingCraftPanel _buildingCraftPanel;    

    public List<CraftSlot> Slot => _slot;

    private PoolMono<CraftSlot> _pool;
    private CraftingSystem _craftingSystem;
    private BuildingStationManager _buildingStationManager;
    private List<CraftingRecipe> _recipe;
    private List<CraftSlot> _slot;

    public event Action<CraftingRecipe> OnGetedRecip;

    private void Start()
    {
        _craftingSystem = GameManager.GetSystem<CraftingSystem>();
        _buildingStationManager = GameManager.GetSystem<BuildingStationManager>();

        _slot = new List<CraftSlot>();
        _recipe = new List<CraftingRecipe>();

        _pool = new PoolMono<CraftSlot>(_slotPrefab, _poolCount, transform);
        _pool.autoExpand = _autoExpande;
    }

    private void ResetPreviousSlot()
    {
        for(int i = 0; i < _slot.Count; i++)
        {
            _slot[i].gameObject.SetActive(false);
        }

        _slot.Clear();
    }
    private void CreateSlots(RectTransform _content,Station station)
    {
        SlotSelection(station);

        ResetPreviousSlot();

        var countSlots = _recipe.Count;
        if (countSlots > 0)
        {
            for (int i = 0; i < countSlots; i++)
            {
                var slot = _pool.GetFreeElement();
                slot.transform.SetParent(_content);
                slot.recipe = _recipe[i];
                 _slot.Add(slot);
                slot.OnClicedOnCraft += OnGetRecipe;
                slot.SetIconSlot();
            }
        }
    }

    private void SlotSelection(Station station)
    {
        _recipe.Clear();

        for (int i = 0; i < _craftingSystem.AllRecipes.Count; i++)
        {
            if (_craftingSystem.AllRecipes[i].requiredStation == station)
                _recipe.Add(_craftingSystem.AllRecipes[i]);
        }
    }

    private void OnGetRecipe(CraftingRecipe recipe)
    {
        OnGetedRecip?.Invoke(recipe);
    }

    private void OnEnable()
    {
        _playerCraft.OnPlayerStationStarted += CreateSlots;
        _buildingCraftPanel.OnBuildPanelChanged += CreateSlots;
    }

    private void OnDisable()
    {
        _playerCraft.OnPlayerStationStarted -= CreateSlots;
        _buildingCraftPanel.OnBuildPanelChanged -= CreateSlots;
    }
}
