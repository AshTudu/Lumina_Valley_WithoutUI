using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Crop " , menuName = " Farming/Crop Data")]
public class CropData : ScriptableObject
{ 
    public string cropName;
    public Sprite[] growthSprite;
    public GameObject harvestedItemPrefab;

    public float timeBetweenStages = 10f;
}
