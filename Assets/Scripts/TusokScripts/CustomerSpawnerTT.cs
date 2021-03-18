using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnerTT : MonoBehaviour
{
  [Header("Customer Spawn")]
  public int spawnInterval;
  public CustomerData[] customers;
  public Seats[] customerSeat;
  private int _customerIndex;


  private void Start()
  {

    foreach (var seat in customerSeat)
    {
      seat.isTaken = false;
    }

    StartCoroutine(SpawnCustomers());

  }

  #region CustomerSpawning

  public void Spawn(CustomerData data)
  {
    var customer = ObjectPoolManager.Instance.GetPooledObject(data.name);
    _customerIndex = Random.Range(0, customerSeat.Length);

    if (isFull()) return;

    if (!customerSeat[_customerIndex].isTaken)
    {
      customer.transform.position = customerSeat[_customerIndex].slot.position;
      customer.GetComponent<CustomerTT>().SeatTaken = _customerIndex;
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
    while (true)
    {
      yield return new WaitForSeconds(spawnInterval);
      Spawn(customers[Random.Range(0, customers.Length)]);
    }
  }
}
#endregion