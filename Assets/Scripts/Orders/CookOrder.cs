using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookOrder : MonoBehaviour
{
    [SerializeField] private Order orderToCook;
    [SerializeField] private Image fillImage;
    private float timer;
    private bool isCooking;

    private void Update()
    {
        CookingIndicator();
    }

    public void StartCooking()
    {
        StartCoroutine(Cook());
    }

    private IEnumerator Cook()
    {
        isCooking = true;
        yield return new WaitForSeconds(orderToCook.Data.CookTime);
        SpawnManager.Instance.Spawn(orderToCook.Data);
        isCooking = false;
    }

    private void CookingIndicator()
    {
        if (isCooking == true)
        {
            timer = orderToCook.Data.CookTime;
            fillImage.fillAmount += 1.0f / timer * Time.deltaTime;
        }
        else if (isCooking == false)
        {
            timer = 0;
            fillImage.fillAmount = timer;
        }
    }

}
