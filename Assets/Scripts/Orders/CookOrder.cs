using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookOrder : MonoBehaviour
{
    [SerializeField] private Order orderToCook;

    #region Pares
    public void StartCooking()
    {
        StartCoroutine(Cook());
    }


    private IEnumerator Cook()
    {
        yield return new WaitForSeconds(orderToCook.Data.CookTime);
        SpawnManager.Instance.Spawn(orderToCook.Data);
    }

    #endregion

    #region TusokTusok

    public void SpawnFood() {

        StartCoroutine(AddFood());
    }
    private IEnumerator AddFood() {
        yield return new WaitForSeconds(orderToCook.Data.CookTime);

    }


    #endregion
}
