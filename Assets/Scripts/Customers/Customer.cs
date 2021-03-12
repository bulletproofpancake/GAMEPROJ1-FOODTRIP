using System.Collections;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerData data;
    [SerializeField] private DialogueData dialogueData;

    [SerializeField] private GameObject orderIcon;
    [SerializeField] private TextMeshPro orderText;

    [SerializeField] private SpriteRenderer dialogueBox;

    private Order _currentOrder;
    private SpriteRenderer _spriteRenderer;

    private float _numberOfOrders;
    private float _completedOrders;

    private float _paymentContainer;
    private bool readyToCollect;
    
    public int SeatTaken { get; set; }

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.ChangeSprite();
        SetOrder();
        GiveOrder();
    }

    private void OnDisable()
    {
        SpawnManager.Instance.customerSeat[SeatTaken].isTaken = false;
        _paymentContainer = 0;
        dialogueBox.color = Color.white;
        orderIcon.GetComponent<SpriteRenderer>().enabled = true;
        readyToCollect = false;
    }
    
    private void SetOrder()
    {
        _completedOrders = 0;
        _numberOfOrders = Random.Range(0, data.PossibleOrders.Length);
    }
    
    private void GiveOrder()
    {
        _currentOrder = data.SelectOrder();
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
                dialogueBox.color = Color.green;
                readyToCollect = true;

                _index = Random.Range(0, dialogueData.customerDialogue.Length);

                orderIcon.GetComponent<SpriteRenderer>().enabled = false;
                orderText.text = dialogueData.customerDialogue[_index].Dialogue;

                StartCoroutine(CustomerDespawn());
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

    private void OnMouseDown()
    {
        if(readyToCollect==true)
        {
            MoneyManager.Instance.Collect(_paymentContainer);
            readyToCollect = false;
            gameObject.SetActive(false);
        }
    }

    //bugs: Customer can still receive orders even when the current orders is complete
    // customer also pays for extra orders that are received
    // For example: Customer orders 2 pares and you give another pares before customer disappears making it 3 pares
    // If you collect the payment in time the customer will pay for 3 pares instead of original 2 pares
}
