using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropData cropData;
    public int currentGrowthStage = 0;
    private SpriteRenderer spriteRenderer;
    private float growthTimer = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialized(CropData data)
    {
        cropData = data;
        currentGrowthStage = 0;
        growthTimer = 0f;
        UpdateVisuals();
    }

    private void Update()
    {
        if (cropData == null || IsFullyGrown()) return;

        growthTimer += Time.deltaTime;

        if (growthTimer >= cropData.timeBetweenStages)
        {
            Grow();
            growthTimer = 0f;
        }
    }

    public void Grow()
    {
        if (!IsFullyGrown())
        {
            currentGrowthStage++;
            UpdateVisuals();
        }
    }

    public bool IsFullyGrown()
    {
        return currentGrowthStage >= cropData.growthSprite.Length - 1;
    }

    private void UpdateVisuals()
    {
        if (cropData != null && cropData.growthSprite.Length > 0)
        {
            spriteRenderer.sprite = cropData.growthSprite[currentGrowthStage];
        }
    }
}
