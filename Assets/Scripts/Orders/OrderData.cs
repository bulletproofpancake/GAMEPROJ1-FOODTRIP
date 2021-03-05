using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Data/New Order")]
public class OrderData : ScriptableObject
{
    [SerializeField] private Sprite orderImage;
    [SerializeField] private int orderCost;
    [SerializeField] private float baseCookTime;
    [SerializeField] private float currentCookTime;

    public Sprite Image => orderImage;
    public int Cost => orderCost;
    public float CookTime => currentCookTime;

    private void OnEnable()
    {
        currentCookTime = baseCookTime;
    }

    public void ReduceCookTime(float multiplier)
    {
        currentCookTime = baseCookTime - (baseCookTime * multiplier);
    }
    
}
