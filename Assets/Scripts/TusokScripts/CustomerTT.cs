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

  [SerializeField]
  private List<OrderTT> currentOrders;

  // sets the current sprite for the order of the customer
  private SpriteRenderer _sprite;
  private float numOfOrders;

  private float _completeOrders;

  private float _payment;

  private bool toCollect;

  CustomerSpawnerTT customertt;

  [SerializeField]
  GameObject dirtyCupSpawnPosition;

  GameObject dirtyCup;



  int fishb, kwek, squidb;
  public int SeatTaken { get; set; }
  void Start()
  {

    customertt = GetComponent<CustomerSpawnerTT>();

    dirtyCupSpawnPosition = GameObject.Find("DirtyCupSpawner");
  }

  private void OnEnable()
  {

    //Upon spawning of character in the scene, select the sprite to use
    _sprite = GetComponent<SpriteRenderer>();
    _sprite.sprite = data.ChangeSprite();
    dirtyCupSpawnPosition = GameObject.Find("DirtyCupSpawner");

    SetOrderTT();
    GiveOrderTT();
    // Debug.Log("Current orders count is: " + currentOrders.Count);
    // Debug.Log("fishb is: " + fishb);
    // Debug.Log("kwek is: " + kwek);
    // Debug.Log("squidb is: " + squidb);
  }

  private void OnDisable()
  {

    //! ERROR: Object reference not set to an instance of an object
    customertt.customerSeat[SeatTaken].isTaken = false;
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
    fishb = 0;
    kwek = 0;
    squidb = 0;

  }

  private void GiveOrderTT()
  {

    for (int i = 0; i < numOfOrders; i++)
    {
      currentOrders.Add(data.GetAllOrder());
      iconOrder[i].GetComponent<SpriteRenderer>().sprite = currentOrders[i].Data.Image;
      Debug.Log("How many are you? " + currentOrders.Count);
    }

    foreach (var order in currentOrders)
    {
      switch (order.tusokFoods)
      {
        case TusokType.Fishball:
          fishb++;
          break;
        case TusokType.Kwekkwek:
          kwek++;
          break;
        case TusokType.Squidball:
          squidb++;
          break;
      }
    }
  }

  private void TakeOrderTT(Cup cups)
  {
    // if function that check if _getOrder contains the same contents as the currentOrders
    //TODO CHANGE lIST.Containts  

    if (cups.fishBalls == fishb)
    {
      Debug.Log("Correct amount of fishballs");
      _completeOrders++;
    }
    if (cups.squidBalls == squidb)
    {
      Debug.Log("Correct amount of squidballs");
      _completeOrders++;
    }
    if (cups.kwekKwek == kwek)
    {
      Debug.Log("Correct amount of  kwekkwek");
      _completeOrders++;
    }
    if (cups.fishBalls != fishb || cups.squidBalls != squidb || cups.kwekKwek != kwek)
    {
      Debug.Log("cups.fishballs amount is: " + cups.fishBalls + " | fishb amount is: " + fishb);
      Debug.Log("cups.squidBalls amount is: " + cups.squidBalls + " | squidb amount is: " + squidb);
      Debug.Log("cups.kwekkwek amount is: " + cups.kwekKwek + " | kwek amount is: " + kwek);
    }

    //* DONUT ERASE

    // orderText.text = $"{_completeOrders}/{numOfOrders + 1}";
    if (_completeOrders == numOfOrders)
    {
      //put lines of script that would approve the order
      int _index = Random.Range(0, dialogeData.customerDialogue.Length);
      for (int i = 0; i < numOfOrders; i++)
      {
        _payment += cups.foodContents[i].Data.Cost;
        // iconOrder[i].GetComponent<SpriteRenderer>().enabled = false;
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
      TakeOrderTT(other.GetComponent<Cup>());
    }
  }


  private IEnumerator CustomerDespawn()
  {

    yield return new WaitForSeconds(data.DespawnTime);
    gameObject.SetActive(false);
    //*Spawns DirtyCup
    dirtyCup = ObjectPoolManager.Instance.GetPooledObject("DirtyCup");
    dirtyCup.SetActive(true);
    dirtyCup.transform.position = dirtyCupSpawnPosition.transform.position;
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
    IconOrderColorChange(Color.red); //sets the color of the icon to red
    yield return new WaitForSeconds(1f);
    IconOrderColorChange(Color.white); //sets it back to the original.
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
