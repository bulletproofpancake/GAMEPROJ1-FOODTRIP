﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Data/New Customer")]
public class CustomerData : ScriptableObject
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Order[] possibleOrders;
    [SerializeField] private int despawnTime;
    
    [SerializeField] private CustomerType type;
    public CustomerType Type => type;

    public Order[] PossibleOrders => possibleOrders;

    public Sprite ChangeSprite()
    {
        return sprites[Random.Range(0, sprites.Length)];
    }

    public Order SelectOrder()
    {
        return possibleOrders[Random.Range(0, possibleOrders.Length)];
    }

    public int DespawnTime => despawnTime;
}

public enum CustomerType
{
    None,
    Student
}