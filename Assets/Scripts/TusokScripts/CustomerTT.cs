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

  private List<OrderTT> currentOrders;

  // sets the current sprite for the order of the customer
  private SpriteRenderer _sprite;
  private float numOfOrders;

  private float _completeOrders;

  private float _payment;

  private bool toCollect;

  [SerializeField]
  Transform dirtyCupSpawnPosition;

  GameObject dirtyCup;
  public int SeatTaken { get; set; }
  void Start()
  {
    //Debug.Log(currentOrders.Count);
  }

  private void OnEnable()
  {
    //Upon spawning of character in the scene, select the sprite to use
    _sprite = GetComponent<SpriteRenderer>();
    _sprite.sprite = data.ChangeSprite();
    Transform transform1 = GameObject.Find("DirtyCupSpawner").transform;
    dirtyCupSpawnPosition = transform1;

    SetOrderTT();
    GiveOrderTT();

  }

  private void OnDisable()
  {
    //! ERROR: Array out of bounds
    SpawnManager.Instance.customerSeat[SeatTaken].isTaken = false;
    _payment = 0;
    dialogueBox.color = Color.white;
    for (int i = 0; i < numOfOrders; i++)
      iconOrder[i].GetComponent<SpriteRenderer>().enabled = true;
    toCollect = false;
  }

  //constant number of orders will be 3
  private void SetOrderTT()
  {
    _completeOrders = 0;
    numOfOrders = 3;

  }

  //! ERROR: LINE 84 currentOrders is apparently not referenced to an object
  private void GiveOrderTT()
  {
    for (int i = 0; i < numOfOrders; i++)
    {
      currentOrders.Add(data.GetAllOrder()); //? bakit walang reference?
      iconOrder[i].GetComponent<SpriteRenderer>().sprite = currentOrders[i].Data.Image;
    }
  }

  private void TakeOrderTT(List<OrderTT> _getOrder)
  {

    // ? make it so that it utilizes List.Contains
    for (int i = 0; i <= numOfOrders; i++)
    {
      // if function that check if _getOrder contains the same contents as the currentOrders
      if (_getOrder.Contains(currentOrders[i]))
      {
        _completeOrders++;
        orderText.text = $"{_completeOrders}/{numOfOrders + 1}";
      }
    }

    if (_completeOrders >= 3)
    {
      //put lines of script that would approve the order
      int _index = Random.Range(0, dialogeData.customerDialogue.Length);
      //? tried separatingthis from the other for loop to make sure that 
      //? payment only counts when the orders are complete.
      for (int i = 0; i < numOfOrders; i++)
      {
        _payment += _getOrder[i].Data.Cost;
        iconOrder[i].GetComponent<SpriteRenderer>().enabled = false;
      }
      orderText.text = dialogeData.customerDialogue[_index].Dialogue;
      StartCoroutine(CustomerDespawn());
    }
    else
    {
      // start coroutine the procedure of not accepting the order
      StartCoroutine(WrongOrder());
    }

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.GetComponent<Cup>())
    {
      TakeOrderTT(other.GetComponent<Cup>().foodContents);
    }
  }


  private IEnumerator CustomerDespawn()
  {

    yield return new WaitForSeconds(data.DespawnTime);
    gameObject.SetActive(false);

    //? spawn the dirty cup as soon as the customer leaves the cart
    //? where to reference dirty cup???????
    dirtyCup = ObjectPoolManager.Instance.GetPooledObject("DirtyCup");
    dirtyCup.SetActive(true);
    SpawnDirtyCup.amountInScene += 1;


  }
  // function that change the color of iconOrders
  private void IconOrderColorChange(Color color)
  {
    for (int i = 0; i < iconOrder.Length; i++)
    {
      iconOrder[i].GetComponent<SpriteRenderer>().color = color;
    }
  }
  private IEnumerator WrongOrder()
  {
    //iconOrder.GetComponent<SpriteRenderer>().color = Color.red;
    IconOrderColorChange(Color.red);
    yield return new WaitForSeconds(1f);
    // iconOrder.GetComponent<SpriteRenderer>().color = Color.white;
    IconOrderColorChange(Color.white);
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
