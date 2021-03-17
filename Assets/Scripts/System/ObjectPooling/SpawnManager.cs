﻿using System.Collections;
using UnityEngine;
/// <summary>
/// Spawn manager for Pares only
/// </summary>
public class SpawnManager : Singleton<SpawnManager>
{
    [Header("Customer Spawn")]
    public int spawnInterval;
    public CustomerData[] customers;
    public Seat[] customerSeat;
    public Transform startPos;
    private int _customerIndex;
    
    [Header("Food Spawn")]
    public Seat[] foodSeat;
    private int _foodIndex;

    [Header("Bowl Spawn")]
    public GameObject bowl;
    public Seat[] bowlSeat;
    private int _bowlIndex;
    
    private void Start()
    {
        foreach (var seat in customerSeat)
        {
            seat.isTaken = false;
        }
        foreach (var seat in foodSeat)
        {
            seat.isTaken = false;
        }

        StartCoroutine(SpawnCustomers());

    }

    #region CustomerSpawning
    
    public void Spawn(CustomerData data)
    {
        float seatPos;

        var customer = ObjectPoolManager.Instance.GetPooledObject(data.name);
        _customerIndex = Random.Range(0, customerSeat.Length);
        
        if (isFull()) return;
        
        if (!customerSeat[_customerIndex].isTaken)
        {
            customer.transform.position = startPos.position;
            //customer.transform.position = customerSeat[_customerIndex].slot.position;
            seatPos = customerSeat[_customerIndex].slot.position.x;

            LeanTween.moveX(customer, seatPos, 2f);

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

        if (counter >= customerSeat.Length)
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

        if (_foodIndex < foodSeat.Length)
        {
            if(_foodIndex < bowlSeat.Length){
                if (bowlSeat[_foodIndex].isTaken)
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
                    Debug.LogWarning("bowl seat taken, looking for another");
                    _foodIndex++;
                    //recursion is done so that
                    //it will continue to spawn
                    //even if seats are taken
                    Spawn(data);
                }
            }
            else
            {
                Debug.LogWarning("No bowls available");
            }
        }
        else
        {
            //there is nothing being spawned
            //because there are no places to spawn
            Debug.LogWarning("Seats full, give orders first");
            _foodIndex = 0;
        }

    }

    #endregion
    
    public void SpawnBowl(GameObject bowl)
    {

        var newBowl = bowl;
        
        if ( _bowlIndex < bowlSeat.Length)
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
}

[System.Serializable]
public class Seat
{
    public Transform slot;
    public bool isTaken;
}
