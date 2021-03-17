using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private OrderData data;
    public OrderData Data => data;

    private SpriteRenderer _spriteRenderer;

    public Bowl _bowl;

    public int SeatTaken { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.Image;
    }

    private void OnDisable()
    {
        SpawnManager.Instance.foodSeat[SeatTaken].isTaken = false;
        if(_bowl!=null){
            SpawnManager.Instance.bowlSeat[_bowl.SeatTaken].isTaken = false;
            _bowl.gameObject.SetActive(false);
        }
        _bowl = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Customer>())
        {
            gameObject.SetActive(false);
        }
    }

    public void GetBowl(Bowl bowl)
    {
        _bowl = bowl;
        _bowl.GetComponent<SpriteRenderer>().enabled = false;
    }
    
}
