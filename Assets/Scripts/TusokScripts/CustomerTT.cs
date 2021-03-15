using System.Collections;
using System.Collections.Generic;
using Customers;
using TMPro;
using UnityEngine;

public class CustomerTT : MonoBehaviour
{

  [SerializeField]
  private CustomerData data;

  [SerializeField]
  private DialogueData dialogeData;

  [SerializeField]
  private GameObject[] iconOrder;

  [SerializeField]
  private TextMeshPro orderText;

  [SerializeField]
  private SpriteRenderer dialogueBox;

  private List<Order> currentOrders;

  // sets the current sprite for the order of the customer
  private SpriteRenderer _sprite;
  private float numOfOrders;

  private float _completeOrders;

  private float _payment;

  private bool toCollect;

  private void OnEnable()
  {
    //Upon spawning of character in the scene, select the sprite to use
    _sprite = GetComponent<SpriteRenderer>();
    _sprite.sprite = data.ChangeSprite();

    SetOrderTT();
    GiveOrderTT();
  }

  //constant number of orders will be 3
  private void SetOrderTT()
  {
    _completeOrders = 0;
    numOfOrders = 3;

  }
  //TODO change all to arrays
  private void GiveOrderTT()
  {
    for (int i = 0; i <= numOfOrders; i++)
    {
      currentOrders[i] = data.GetAllOrder(i);
      iconOrder[i].GetComponent<SpriteRenderer>().sprite = currentOrders[i].Data.Image;
    }
  }

  // TODO CHANGE CURRENTORDERS TO PROBABLY INTO AN ARRAY
  private void TakeOrderTT(Order givenOrder)
  {
    int currentIndex;
    for (int i = 0; i <= numOfOrders; i++)
    {
      if (currentOrders[i].Data == givenOrder.Data)
      {
        _completeOrders++;
        _payment += givenOrder.Data.Cost;
      }
    }

    //if currentOrders is equal to the given orders
    if (currentOrders.Data == givenOrder.Data)
    {
      _completeOrders++;
      _payment += givenOrder.Data.Cost;

      // if all orders are complete
      if (_completeOrders >= numOfOrders + 1)
      {
        dialogueBox.color = Color.green;
        toCollect = true;
        currentIndex = Random.Range(0, dialogeData.customerDialogue.Length);

        iconOrder.GetComponent<SpriteRenderer>().enabled = false;
        orderText.text = dialogeData.customerDialogue[currentIndex].Dialogue;

        StartCoroutine(CustomerDespawn());
      }
      else
      {
        StartCoroutine(WrongOrder());
      }
    }
  }

  //TODO Make a function that when the customer despawns, it would leave a dirty cup. Prefereably IEnumerator. REFERENCE: Customer.cs LINE: 108
  private IEnumerator CustomerDespawn()
  {
    yield return new WaitForSeconds(data.DespawnTime);
    gameObject.SetActive(false);

    // TODO add a line of code where in it spawns the dirty cup
  }
  private IEnumerator WrongOrder()
  {
    iconOrder.GetComponent<SpriteRenderer>().color = Color.red;
    yield return new WaitForSeconds(1f);
    iconOrder.GetComponent<SpriteRenderer>().color = Color.white;
  }

  private void OnMouseDown()
  {
    if (toCollect == true)
    {
      MoneyManager.Instance.Collect(_payment);
      toCollect = false;
      gameObject.SetActive(false);
    }
  }


}
