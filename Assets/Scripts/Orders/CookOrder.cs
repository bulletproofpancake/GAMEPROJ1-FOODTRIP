using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookOrder : MonoBehaviour
{
    [SerializeField] private Order orderToCook;
    [SerializeField] private Image fillImage;
    private Button button;
    private float timer;
    private bool isCooking;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        CookingIndicator();
        if (Pause.isPaused == true)
            button.interactable = false;
        else if (Pause.isPaused == false)
            button.interactable = true;
        //CookingIndicator();
    }

    #region Pares
    public void StartCooking()
    {
        if (Pause.isPaused == true)
            StopAllCoroutines();
        else
            StartCoroutine(Cook());
    }


    private IEnumerator Cook()
    {
        isCooking = true;
        //switch (orderToCook.Data.Type)
        //{
        //    case OrderType.Pares:
        //        FindObjectOfType<AudioManager>().Play("ParesSFX");
        //        yield return new WaitForSeconds(orderToCook.Data.CookTime);
        //        FindObjectOfType<AudioManager>().Stop("ParesSFX");
        //        break;

        //    case OrderType.Kanin:
        //        FindObjectOfType<AudioManager>().Play("KaninSFX");
        //        yield return new WaitForSeconds(orderToCook.Data.CookTime);
        //        FindObjectOfType<AudioManager>().Stop("KaninSFX");
        //        break;
        //}

        yield return new WaitForSeconds(orderToCook.Data.CookTime);
        SpawnManager.spawner.Spawn(orderToCook.Data);
        isCooking = false;

        if(FindObjectOfType<AudioManager>()!=null){
            FindObjectOfType<AudioManager>().Play("OrderReady");
        }
    }

    #endregion

    #region TusokTusok

    public void SpawnFood() {

        StartCoroutine(AddFood());
    }
    private IEnumerator AddFood() {
        yield return new WaitForSeconds(orderToCook.Data.CookTime);
        TusokTusokFoodSpawner.Instance.Spawn(orderToCook.Data);
    }


    #endregion

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
