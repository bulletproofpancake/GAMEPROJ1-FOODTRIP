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
        BowlSpawner.spawner.RemoveBowl(gameObject);
    }

    private void OnDisable()
    {
        if(isDirty)
            Sink.sink.AddBowl(gameObject);
        else
            BowlSpawner.spawner.AddBowl(gameObject); 
        SpawnManager.spawner.bowlSeat[SeatTaken].isTaken = false;
    }

    public void ReduceWashTime(float multiplier)
    {
        currentWashTime = baseWashTime - baseWashTime * multiplier;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Order>())
        {
            var order = other.GetComponent<Order>();
            if (order._bowl == null)
            {
                isDirty = true;
                order.GetBowl(this);
            }
        }
    }
}
