using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Data/New Customer")]
public class CustomerData : ScriptableObject
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Order[] possibleOrders;
    public Order[] PossibleOrders => possibleOrders;

    public Sprite ChangeSprite()
    {
        return sprites[Random.Range(0, sprites.Length)];
    }
}