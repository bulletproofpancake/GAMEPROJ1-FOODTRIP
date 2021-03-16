using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Data/New Customer")]
public class CustomerData : ScriptableObject
{
  [SerializeField] private Sprite[] sprites;
  [SerializeField] private Order[] possibleOrders;

  [SerializeField] private List<OrderTT> possibleOrder;
  [SerializeField] private int despawnTime;

  public Order[] PossibleOrders => possibleOrders;


  //vairables with 'TT' at the end are for tusok tusok
  public List<OrderTT> PossibleOrder => possibleOrder;

  private OrderTT[] orderLineUp;



  public Sprite ChangeSprite()
  {
    return sprites[Random.Range(0, sprites.Length)];
  }

  public Order SelectOrder()
  {
    return possibleOrders[Random.Range(0, possibleOrders.Length)];
  }

  // * Use this function to get all orders of customer specifically in Tusok Tusok


  public OrderTT GetAllOrder()
  {
    return possibleOrder[Random.Range(0, possibleOrder.Count)];
  }

  public int DespawnTime => despawnTime;
}