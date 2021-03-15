using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Data/New Customer")]
public class CustomerData : ScriptableObject
{
  [SerializeField] private Sprite[] sprites;
  [SerializeField] private Order[] possibleOrders;

  [SerializeField] private OrderTT[] possibleOrder;
  [SerializeField] private int despawnTime;

  public Order[] PossibleOrders => possibleOrders;


  //vairables with 'TT' at the end are for tusok tusok
  public OrderTT[] PossibleOrder => possibleOrder;

  private OrderTT[] OrderLineUp = new OrderTT[3];

  public Sprite ChangeSprite()
  {
    return sprites[Random.Range(0, sprites.Length)];
  }

  public Order SelectOrder()
  {
    return possibleOrders[Random.Range(0, possibleOrders.Length)];
  }

  // * Use this function to get all orders of customer
  public OrderTT GetAllOrder(int index)
  {
    for (int i = 0; i < possibleOrders.Length; i++)
    {
      OrderLineUp[i] = possibleOrder[Random.Range(0, possibleOrder.Length)];
    }
    return OrderLineUp[index];
  }

  public int DespawnTime => despawnTime;
}