using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerData data;
    [SerializeField] private GameObject orderIcon;
    [SerializeField] private Order currentOrder;
    
    private SpriteRenderer _spriteRenderer;
    
    public int SeatTaken { get; set; }
    
    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.ChangeSprite();
        GiveOrder();
    }

    private void OnDisable()
    {
        SpawnManager.Instance.customerSeat[SeatTaken].isTaken = false;
    }

    private void GiveOrder()
    {
        currentOrder = data.SelectOrder();
        orderIcon.GetComponent<SpriteRenderer>().sprite = currentOrder.Data.Image;
    }
}
