using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    [SerializeField] private float baseWashTime;
    public float currentWashTime;
    public bool isDirty;
    public int SeatTaken { get; set; }

    private void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        BowlSpawner.Instance.RemoveBowl(gameObject);
    }

    private void OnDisable()
    {
        SpawnManager.Instance.bowlSeat[SeatTaken].isTaken = false;
        if(isDirty)
            Sink.Instance.AddBowl(gameObject);
        else
            BowlSpawner.Instance.AddBowl(gameObject); 
    }

    public void ReduceWashTime(float multiplier)
    {
        currentWashTime = baseWashTime - baseWashTime * multiplier;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Order>())
        {
            isDirty = true;
            other.GetComponent<Order>().GetBowl(this);
        }
    }
}
