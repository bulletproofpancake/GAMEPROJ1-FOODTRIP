using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Data/New Order")]
public class OrderData : ScriptableObject
{
    [SerializeField] private Sprite orderImage;
    [SerializeField] private int orderCost;
    [SerializeField] private int orderCookTime;

    public Sprite Image => orderImage;
    public int Cost => orderCost;
    public int CookTime => orderCookTime;
}
