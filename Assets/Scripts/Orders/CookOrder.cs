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
        if (Pause.isPaused == true)
            button.interactable = false;
        else if (Pause.isPaused == false)
            button.interactable = true;
        CookingIndicator();
    }

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
        isCooking = false;

        FindObjectOfType<AudioManager>().Play("OrderReady");
    }

    private void CookingIndicator()
    {
        if(Pause.isPaused==true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

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

    //bugs: If you cook two orders of the SAME TYPE
    // once the first order is done cooking while the second order is still cooking
    // the second order's cooking audio will also stop since they have the same AudioSource
}
