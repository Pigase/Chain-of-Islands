using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOpenerPanelDescription : MonoBehaviour
{
    [SerializeField] private StationKeeper stationKeeper;
    [SerializeField] private RectTransform _content;
    [SerializeField] private IngridientSlot _prefabIngridientSlot;

    private ItemDataBase _itemData;
    private int _poolCount = 20;
    private bool _autoExpande = true;
    private PoolMono<IngridientSlot> _pool;
    private List<IngridientSlot> _iconsByIngridients;

    private BuildingStation _building;
    
    public event Func<BuildingStation> OnBuildingOpenerPanelOnEnable;

    private void Awake()
    {
        _itemData = GameManager.GetSystem<ItemDataBase>();
        _iconsByIngridients = new List<IngridientSlot>();

        _pool = new PoolMono<IngridientSlot>(_prefabIngridientSlot, _poolCount, _content.transform);
        _pool.autoExpand = _autoExpande;
    }

    private void OnEnable()
    {
        _building = OnBuildingOpenerPanelOnEnable?.Invoke();

        DataPurpose(_building);
    }
    private void DataPurpose(BuildingStation building)
    {
        ResetIngridients();

        IngridientsIconDisplay(_building.buildingStation.ingredients.Count, building);

    }
    private void ResetIngridients()
    {
        for (int i = 0; i < _iconsByIngridients.Count; i++)
        {
            _iconsByIngridients[i].gameObject.SetActive(false);
        }

        _iconsByIngridients.Clear();
    }

    private void IngridientsIconDisplay(int amountSlot, BuildingStation building)
    {
        for (int i = 0; i < amountSlot; i++)
        {
            Item ingridientInfo = _itemData.GetItem(building.buildingStation.ingredients[i].itemId);

            IngridientSlot ingridient = _pool.GetFreeElement();
            ingridient.transform.SetParent(_content);
            ingridient.transform.localScale = Vector3.one;
            ingridient.icon.sprite = ingridientInfo.icon;
            ingridient.text.text = building.buildingStation.ingredients[i].amount <= 1 ? "" : building.buildingStation.ingredients[i].amount.ToString();

            _iconsByIngridients.Add(ingridient);
        }
    }
}
