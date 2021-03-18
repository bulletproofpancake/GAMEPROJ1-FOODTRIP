using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookOrderTT : MonoBehaviour
{
  [SerializeField] private OrderTT orderToCook;
  [SerializeField] private Image fillImage;

  private Button button;
  private float timer;
  private bool isCooking;

  private void Start()
  {
    timer = 1.0f;
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
    yield return new WaitForSeconds(orderToCook.Data.CookTime);
    SpawnManager.Instance.Spawn(orderToCook.Data);
    isCooking = false;
  }

  #endregion

  #region TusokTusok

  public void SpawnFood()
  {

    StartCoroutine(AddFood());
  }
  private IEnumerator AddFood()
  {
    isCooking = true;
    yield return new WaitForSeconds(orderToCook.Data.CookTime);
    TusokTusokFoodSpawner.Instance.Spawn(orderToCook.Data);
    isCooking = false;
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
