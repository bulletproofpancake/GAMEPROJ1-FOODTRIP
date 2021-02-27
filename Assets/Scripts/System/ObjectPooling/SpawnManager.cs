using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : Singleton<SpawnManager>
{
    public CustomerData[] customers;
    public Seat[] customerSeat;
    public int spawnInterval;
    private int _customerIndex;
    
    public Seat[] foodSeat;
    private int _foodIndex;

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
    
    public void Spawn(CustomerData data)
    {
        var customer = ObjectPoolManager.Instance.GetPooledObject(data.name);
        _customerIndex = Random.Range(0, customerSeat.Length);
        
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

    private IEnumerator SpawnCustomers()
    {
        yield return new WaitForSeconds(spawnInterval);
        Spawn(customers[Random.Range(0,customers.Length)]);
    }

    public void Spawn(OrderData data)
    {
        var food = ObjectPoolManager.Instance.GetPooledObject(data.name);

        if (_foodIndex < foodSeat.Length)
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
            _foodIndex = 0;
        }

    }

}

[System.Serializable]
public class Seat
{
    public Transform slot;
    public bool isTaken;
}
