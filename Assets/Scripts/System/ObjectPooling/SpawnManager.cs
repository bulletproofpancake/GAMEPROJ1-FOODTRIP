﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Spawn manager for Pares only
/// </summary>
public class SpawnManager : Singleton<SpawnManager>
{
    [Header("NPC Spawn")]
    public NPCData[] NpcDatas;

    [Header("Customer Spawn")]
    public int spawnInterval;
    public CustomerData[] customers;
    public List<Seat> customerSeat;
    private int _customerIndex;
    
    [Header("Food Spawn")]
    public List<Seat> foodSeat;
    private int _foodIndex;

    [Header("Bowl Spawn")]
    public GameObject bowl;
    public List<Seat> bowlSeat;
    private int _bowlIndex;

    
    public List<Seat> seats;
    
    protected override void Awake()
    {
        _customerIndex = 0;
        _foodIndex = 0;
        seats = new List<Seat>(FindObjectsOfType<Seat>());
        foreach (var seat in seats)
        {
            seat.isTaken = false;
            switch (seat.type)
            {
                case SeatType.Customer:
                    customerSeat.Add(seat);
                    break;
                case SeatType.Food:
                    foodSeat.Add(seat);
                    bowlSeat.Add(seat);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void Start()
    {
        if(!GameManager.Instance.isVN){
            StartCoroutine(SpawnCustomers());
        }
        else
        {
            SpawnVN();
        }
    }

    #region CustomerSpawning
    private void SpawnVN()
    {
        var npc = ScriptableObject.CreateInstance<NPCData>();
        
        foreach (var data in NpcDatas)
        {
            if(ShiftManager.Instance.shift != null){
                if (data.AppearsIf == ShiftManager.Instance.shift.Schedule)
                {
                    npc = data;
                }
            }
            else
            {
                Debug.LogWarning("Loading default npc");
                npc = NpcDatas[0];
            }
        }
        
        var customer = ObjectPoolManager.Instance.GetPooledObject(npc.name);

        if (customer == null)
        {
            Debug.LogWarning("No NPC object found");
            return;
        }

        _customerIndex = Random.Range(0, customerSeat.Count);
        
        if (isFull()) return;
        
        if (!customerSeat[_customerIndex].isTaken)
        {
            customer.transform.position = customerSeat[_customerIndex].slot.position;
            customer.GetComponent<Customer>().SeatTaken = _customerIndex;
            customer.SetActive(true);
            customerSeat[_customerIndex].isTaken = true;
        }
        else
        {
            Debug.LogWarning("Seat taken, looking for another");
        }
    }
    
    private void Spawn(CustomerData data)
    {
        var customer = ObjectPoolManager.Instance.GetPooledObject(data.name);
        _customerIndex = Random.Range(0, customerSeat.Count);
        
        if (isFull()) return;
        
        if (!customerSeat[_customerIndex].isTaken)
        {
            customer.transform.position = customerSeat[_customerIndex].slot.position;
            customer.GetComponent<Customer>().SeatTaken = _customerIndex;
            customer.SetActive(true);
            customerSeat[_customerIndex].isTaken = true;
        }
        else
        {
            Debug.LogWarning("Seat taken, looking for another");
        }
    }

    private bool isFull()
    {
        var counter = 0;
        
        foreach (var seat in customerSeat)
        {
            if (seat.isTaken)
            {
                counter++;
            }
        }

        if (counter >= customerSeat.Count)
            return true;
        else
            return false;
    }

    private IEnumerator SpawnCustomers()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Spawn(customers[Random.Range(0,customers.Length)]);
        }
    }

    #endregion

    #region FoodSpawning

    public void Spawn(OrderData data)
    {
        var food = ObjectPoolManager.Instance.GetPooledObject(data.name);

        if (_foodIndex < foodSeat.Count)
        {
            if (foodSeat[_foodIndex] != null)
            {
                if (!foodSeat[_foodIndex].isTaken)
                {
                    food.transform.position = foodSeat[_foodIndex].slot.position;
                    food.GetComponent<Order>().SeatTaken = _foodIndex;
                    food.SetActive(true);
                    foodSeat[_foodIndex].isTaken = true;
                    //index always starts at zero so that
                    //all slots can be checked
                    _foodIndex = 0;
                }
                else
                {
                    Debug.LogWarning("Seat taken, looking for another");
                    _foodIndex++;
                    //recursion is done so that
                    //it will continue to spawn
                    //even if seats are taken
                    Spawn(data);
                }
            }
            else
            {
                //there is nothing being spawned
                //because there are no places to spawn
                Debug.LogWarning("Seats full, give orders first");
                print(foodSeat.Count);
                _foodIndex = 0;
            }
        }

    }

    #endregion
    
    public void SpawnBowl(GameObject bowl)
    {

        var newBowl = bowl;
        
        if ( _bowlIndex < bowlSeat.Count)
        {
            if (!bowlSeat[_bowlIndex].isTaken)
            {
                newBowl.transform.position = bowlSeat[_bowlIndex].slot.position;
                newBowl.GetComponent<Bowl>().SeatTaken = _bowlIndex;
                newBowl.SetActive(true);
                bowlSeat[_bowlIndex].isTaken = true;
                _bowlIndex = 0;
            }
            else
            {
                Debug.LogWarning("Seat taken, looking for another");
                _bowlIndex++;
                //recursion is done so that
                //it will continue to spawn
                //even if seats are taken
                SpawnBowl(bowl);
            }
        }
        else
        {
            Debug.LogWarning("Seats full, give orders first");
            _bowlIndex = 0;
        }
    }

    public void ClearLists()
    {
        customerSeat.Clear();
        foodSeat.Clear();
        bowlSeat.Clear();
    }
    
}
