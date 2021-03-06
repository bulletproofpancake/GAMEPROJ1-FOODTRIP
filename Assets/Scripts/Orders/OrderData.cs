using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Data/New Order")]
public class OrderData : ScriptableObject
{
    [SerializeField] private Sprite orderImage;
    [SerializeField] private float baseOrderCost;
    [SerializeField] private float currentOrderCost;
    [SerializeField] private float baseCookTime;
    [SerializeField] private float currentCookTime;

    public Sprite Image => orderImage;
    public float Cost => currentOrderCost;
    public float CookTime => currentCookTime;

    private void OnEnable()
    {
        currentOrderCost = baseOrderCost;
        currentCookTime = baseCookTime;
    }

    public void ReduceCookTime(float multiplier)
    {
        currentCookTime = baseCookTime - (baseCookTime * multiplier);
    }

    public void IncreaseOrderCost(float multiplier)
    {
        currentOrderCost = baseOrderCost + (baseOrderCost * multiplier);
    }
}
