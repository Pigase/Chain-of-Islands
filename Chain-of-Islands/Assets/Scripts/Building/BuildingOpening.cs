using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOpening : MonoBehaviour
{
    [SerializeField] private BuildingStation _buildingStation;

    [Range(0f, 1f)]
    [SerializeField] private float alpha = 0.3f;

    private SpriteRenderer[] spriteRenderers;
    private float lastAlpha;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        lastAlpha = alpha;

        SetTransparency(lastAlpha);
    }
    private void OpenBuilding()
    {
        lastAlpha = 1;
        SetTransparency(lastAlpha);
    }
    public void SetTransparency(float alpha)
    {
        foreach (SpriteRenderer sprite in spriteRenderers)
        {
            Color color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }
    }

    private void OnEnable()
    {
        _buildingStation.OnBuildingOpened += OpenBuilding;
    }

    private void OnDisable()
    {
        _buildingStation.OnBuildingOpened -= OpenBuilding;
    }
}
