using System;
using System.Collections;
using Customers.Dialogue;
using Customers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    private NPCData _npcData;
    [SerializeField] private CustomerData data;
    public CustomerData Data => data;
    [SerializeField] private GameObject orderIcon;
    [SerializeField] private TextMeshPro orderText;
    public GameObject orderBox;
    
    [SerializeField] private Customers.DialogueData dialogueData;
    [SerializeField] private SpriteRenderer dialogueBox;
    [SerializeField] private Sprite DialogueBoxNormal;
    [SerializeField] private Sprite dialogueBoxPaid;

    [SerializeField] private Transform originalPos;
    [SerializeField] private Transform endPos;

    private Order _currentOrder;
    private SpriteRenderer _spriteRenderer;

    private float _numberOfOrders;
    private float _completedOrders;

    private float _paymentContainer;
    private bool readyToCollect;
    
    public int SeatTaken { get; set; }


    public GameObject particleEffect;
    
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
            OrderPares();
        }
    }

    private void OnDisable()
    {
        particleEffect.SetActive(false);
        SpawnManager.Instance.customerSeat[SeatTaken].isTaken = false;
        _paymentContainer = 0;
        dialogueBox.sprite = DialogueBoxNormal;
        orderIcon.GetComponent<SpriteRenderer>().enabled = true;
        readyToCollect = false;

        gameObject.transform.position = originalPos.position;
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

    public void OrderPares()
    {
        _currentOrder = data.PossibleOrders[0];
        orderIcon.GetComponent<SpriteRenderer>().sprite = _currentOrder.Data.Image;
        orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
    }

    private void TakeOrder(Order givenOrder)
    {

        if (_currentOrder.Data == givenOrder.Data)
        {
            StartCoroutine(ShowParticleEffect());
            _completedOrders++;

            _paymentContainer += givenOrder.Data.Cost;

            orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";

            if (_completedOrders >= _numberOfOrders + 1)
            {
                FindObjectOfType<AudioManager>().Play("OrdersComplete");
                dialogueBox.sprite = dialogueBoxPaid;
                readyToCollect = true;
                
                if (GameManager.Instance.isVN)
                {
                    _npcData = (NPCData) data;
                    if (_npcData.Encounter[_npcData.Count].DialogueCount > DialogueManager.Instance.dataIndex)
                    {
                        orderBox.SetActive(false);
                        SetOrder();
                        GiveOrder();
                        DialogueManager.Instance.Advance();
                    }
                    else
                    {
                        OrderPrompt();
                    }
                    
                }
                else
                {
                    OrderPrompt();
                    StartCoroutine(CustomerDespawn());
                }
            }
            else
            {
                GiveOrder();
            }
        }
        else
        {
            StartCoroutine(WrongOrder());
        }
    }

    private void OrderPrompt()
    {
        int _index = Random.Range(0, dialogueData.customerDialogue.Length);

        orderIcon.GetComponent<SpriteRenderer>().enabled = false;
        orderText.text = dialogueData.customerDialogue[_index].Dialogue;
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
        LeanTween.moveX(gameObject, endPos.position.x, 2f);
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (readyToCollect == true)
        {
            MoneyManager.Instance.Collect(_paymentContainer);
            _paymentContainer = 0;

            if (GameManager.Instance.isVN)
            {
                MoneyManager.Instance.Earn();
                _npcData.IncrementEncounter();
                GameManager.Instance.customers.Remove(this);
                GameManager.Instance.completedCustomers++;
            }

            orderBox.SetActive(false);
            StartCoroutine(CustomerLeave());
        }
    }

    private IEnumerator CustomerLeave()
    {
        LeanTween.moveX(gameObject, endPos.position.x, data.DespawnTime);
        yield return new WaitForSeconds(data.DespawnTime);

        gameObject.SetActive(false);
    }

    private IEnumerator ShowParticleEffect()
    {
        particleEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        particleEffect.SetActive(false);
    }
}
