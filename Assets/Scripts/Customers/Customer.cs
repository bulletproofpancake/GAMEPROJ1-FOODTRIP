using System.Collections;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerData data;
    [SerializeField] private GameObject orderIcon;
    [SerializeField] private TextMeshPro orderText;
    
    private Order _currentOrder;
    private SpriteRenderer _spriteRenderer;

    private float _numberOfOrders;
    private float _completedOrders;
    
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
        if (_currentOrder.Data == givenOrder.Data)
        {
            _completedOrders++;
            orderText.text = $"{_completedOrders}/{_numberOfOrders + 1}";
            MoneyManager.Instance.Collect(_currentOrder.Data.Cost);
            if (_completedOrders >= _numberOfOrders + 1)
            {
                gameObject.SetActive(false);
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
