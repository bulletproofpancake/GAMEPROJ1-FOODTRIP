using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private OrderData data;
    public OrderData Data => data;

    private SpriteRenderer _spriteRenderer;

    public int SeatTaken { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.Image;
    }

    private void OnDisable()
    {
        SpawnManager.Instance.foodSeat[SeatTaken].isTaken = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Customer>())
        {
            gameObject.SetActive(false);
        }
    }
}
