using System;
using System.Collections;
using Customers.Dialogue;
using Customers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerData data;
    public CustomerData Data => data;
    [SerializeField] private GameObject orderIcon;
    [SerializeField] private TextMeshPro orderText;
    public GameObject orderBox;
    
    [SerializeField] private Customers.DialogueData dialogueData;
    [SerializeField] private SpriteRenderer dialogueBox;

    private Order _currentOrder;
    private SpriteRenderer _spriteRenderer;

    private float _numberOfOrders;
    private float _completedOrders;

    private float _paymentContainer;
    private bool readyToCollect;
    
    public int SeatTaken { get; set; }

    private void Start()
    {
        if (GameManager.Instance.isVN)
        {
            orderBox.SetActive(false);
            GameManager.Instance.customers.Add(this);
            DialogueManager.Instance.GetCustomerObject(gameObject);
            DialogueManager.Instance.Advance();
        }
    }

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.ChangeSprite();
        
        if (!GameManager.Instance.isVN)
        {
            SetOrder();
            GiveOrder();
        }
    }

    private void OnDisable()
    {
        SpawnManager.Instance.customerSeat[SeatTaken].isTaken = false;
        _paymentContainer = 0;
        dialogueBox.color = Color.white;
        orderIcon.GetComponent<SpriteRenderer>().enabled = true;
        readyToCollect = false;
    }
    
    public void SetOrder()
    {
        _completedOrders = 0;
        _numberOfOrders = Random.Range(0, data.PossibleOrders.Length);
    }
    
    public void GiveOrder()
    {
        _currentOrder = data.SelectOrder();
        orderIcon.GetComponent<SpriteRenderer>().sprite = _currentOrder.Data.Image;
        orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
    }

    public void GiveOrder(Order order)
    {
        _currentOrder = order;
        orderIcon.GetComponent<SpriteRenderer>().sprite = _currentOrder.Data.Image;
        orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
    }

    private void TakeOrder(Order givenOrder)
    {
        int _index;

        if (_currentOrder.Data == givenOrder.Data)
        {
            _completedOrders++;

            _paymentContainer += givenOrder.Data.Cost;

            orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";

            if (_completedOrders >= _numberOfOrders + 1)
            {
                if (GameManager.Instance.isVN)
                {
                    var npcData = (NPCData) data;
                    if (npcData.Encounter[npcData.Count].DialogueCount > DialogueManager.Instance.dataIndex)
                    {
                        orderBox.SetActive(false);
                        SetOrder();
                        GiveOrder();
                        DialogueManager.Instance.Advance();
                    }
                    else
                    {
                        dialogueBox.color = Color.green;
                        readyToCollect = true;

                        _index = Random.Range(0, dialogueData.customerDialogue.Length);

                        orderIcon.GetComponent<SpriteRenderer>().enabled = false;
                        orderText.text = dialogueData.customerDialogue[_index].Dialogue;

                        StartCoroutine(NPCDespawn(npcData));
                    }
                    
                }
                else
                {
                    dialogueBox.color = Color.green;
                    readyToCollect = true;

                    _index = Random.Range(0, dialogueData.customerDialogue.Length);

                    orderIcon.GetComponent<SpriteRenderer>().enabled = false;
                    orderText.text = dialogueData.customerDialogue[_index].Dialogue;

                    StartCoroutine(CustomerDespawn());
                }
            }
        }
        else
        {
            StartCoroutine(WrongOrder());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Order>())
        {
            TakeOrder(other.GetComponent<Order>());
        }
    }

    private IEnumerator WrongOrder()
    {
        orderIcon.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        orderIcon.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator CustomerDespawn()
    {
        yield return new WaitForSeconds(data.DespawnTime);

        gameObject.SetActive(false);
    }

    private IEnumerator NPCDespawn(NPCData npcData){
        yield return new WaitForSeconds(data.DespawnTime);
        npcData.IncrementEncounter();
        GameManager.Instance.customers.Remove(this);
        GameManager.Instance.completedCustomers++;
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if(readyToCollect==true)
        {
            MoneyManager.Instance.Collect(_paymentContainer);
            readyToCollect = false;
            gameObject.SetActive(false);
        }
    }
}
