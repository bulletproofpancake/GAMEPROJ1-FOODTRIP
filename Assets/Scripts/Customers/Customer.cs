using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerData data;
    private SpriteRenderer _spriteRenderer;
    public int SeatTaken { get; set; }
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.ChangeSprite();
    }
    

    private void OnDisable()
    {
        SpawnManager.Instance.customerSeat[SeatTaken].isTaken = false;
    }
}
