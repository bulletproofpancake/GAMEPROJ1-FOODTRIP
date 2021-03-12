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
        switch(orderToCook.Data.Type)
        {
            case OrderType.Pares:
                FindObjectOfType<AudioManager>().Play("ParesSFX");
                yield return new WaitForSeconds(orderToCook.Data.CookTime);
                FindObjectOfType<AudioManager>().Stop("ParesSFX");
                break;

            case OrderType.Kanin:
                FindObjectOfType<AudioManager>().Play("KaninSFX");
                yield return new WaitForSeconds(orderToCook.Data.CookTime);
                FindObjectOfType<AudioManager>().Stop("KaninSFX");
                break;
        }

        SpawnManager.Instance.Spawn(orderToCook.Data);

        FindObjectOfType<AudioManager>().Play("OrderReady");
    }

    //bugs: If you cook two orders of the SAME TYPE
    // once the first order is done cooking while the second order is still cooking
    // the second order's cooking audio will also stop since they have the same AudioSource
}
