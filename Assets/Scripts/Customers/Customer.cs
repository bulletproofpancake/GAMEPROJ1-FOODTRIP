using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerData data;
    [SerializeField] private GameObject orderIcon;
    [SerializeField] private TextMeshPro orderText;
    public GameObject orderBox;
    
    private Order _currentOrder;
    private SpriteRenderer _spriteRenderer;

    private float _numberOfOrders;
    private float _completedOrders;
    
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
        //SetOrder();
        //GiveOrder();
    }

    private void OnDisable()
    {
        SpawnManager.Instance.customerSeat[SeatTaken].isTaken = false;
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
        if (_currentOrder.Data == givenOrder.Data)
        {
            _completedOrders++;
            orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
            MoneyManager.Instance.Collect(_currentOrder.Data.Cost);
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
                        npcData.IncrementEncounter();
                        GameManager.Instance.customers.Remove(this);
                        GameManager.Instance.completedCustomers++;
                        gameObject.SetActive(false);
                    }
                    
                }
                else
                {
                    gameObject.SetActive(false);
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
    
}
