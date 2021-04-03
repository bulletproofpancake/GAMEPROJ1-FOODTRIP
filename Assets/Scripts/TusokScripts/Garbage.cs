using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garbage : MonoBehaviour
{

     [Tooltip("this indicated the number of trashes can be stored")]
     [Range(5, 10)]
     public int trashCapacity;

     [Tooltip("Change how long for the trash to be replaced")]
     [Range(1, 4)]
     public float timeForChange;

     private float currentCapacity;
     public static bool isFull;


     [Tooltip("Put all the sprites needed for the garbage states")]
     [SerializeField]
     private Sprite[] GarbageStates;

     private int indexState;


    private Button btn;

    [SerializeField]
    private Image fllimg;

    private bool isRefill;

     private void Awake()
     {
        isRefill = false;
        btn = this.gameObject.GetComponent<Button>();
     }

     private void OnEnable()
     {
          ResetStats();
     }

    private void Update()
    {
        if (Pause.isPaused == true)
            btn.interactable = false;
        else if (Pause.isPaused == false)
            btn.interactable = true;

        CapacityChecker();
    }

    private void ResetStats()
     {

          currentCapacity = 0;
          btn.image.sprite = GarbageStates[0];

          indexState = -1;
          isFull = false;

     }

    private void SpriteFilling()
    {
        float timer;
        if (isRefill)
        {
            timer = timeForChange;
            fllimg.fillAmount += 1.0f / timer * Time.deltaTime;
        }
        else
        {
            timer = 0;
            fllimg.fillAmount = timer;
        }
    }

     //Added parameters incase we would dispose food or sticks here and have diff value
     public void CapacityChecker()
     {
        float capPerc = DirtyCupsScript.Instance.currentDirtyCupsInScene / DirtyCupsScript.Instance.maxAmountInScene 
            * 100.0f;
        GarbageSpriteChange(capPerc);
     }

     //make a function that would do the process of changing trash sprites
     private void GarbageSpriteChange(float percentage)
     {

          for (int i = 0; i <= percentage; i += 25)
          {
               indexState++;
          }
          Debug.Log("value of indexState is " + indexState);
          btn.image.sprite = GarbageStates[indexState];

     }


     public void ChangeGarbage()
     {
        Debug.Log("You press the change the garbge");
          StartCoroutine(ChangeGarbageProcess());
     }

     //make a function that would do the procress of changing trash
     private IEnumerator ChangeGarbageProcess()
     {
         isRefill = true;
          yield return new WaitForSeconds(timeForChange);
          DirtyCupsScript.Instance.currentDirtyCupsInScene = 0;
          DirtyCupsScript.Instance.RemoveDirtyCups();
          ResetStats();

     }



}

