using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Seat[] foodSeat;
    private int _foodIndex;

    private void Start()
    {
        foreach (var seat in foodSeat)
        {
            seat.isTaken = false;
        }
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
