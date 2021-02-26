using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookOrder : MonoBehaviour
{
    [SerializeField] private Order orderToCook;

    public void StartCooking()
    {
        StartCoroutine(Cook());
    }

    private IEnumerator Cook()
    {
        yield return new WaitForSeconds(orderToCook.Data.CookTime);
        SpawnManager.Instance.Spawn(orderToCook.Data);
    }
}
