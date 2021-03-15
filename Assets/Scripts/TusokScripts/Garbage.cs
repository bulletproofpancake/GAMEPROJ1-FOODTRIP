using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{

  [Tooltip("this indicated the number of trashes can be stored")]
  [Range(5, 10)]
  public int trashCapacity;

  [Tooltip("Change how long for the trash to be replaced")]
  [Range(1, 4)]
  public float timeForChange;

  private float currentCapacity;
  private bool isFull;


  [Tooltip("Put all the sprites needed for the garbage states")]
  [SerializeField]
  private Sprite[] GarbageStates;

  private int indexState;

  private SpriteRenderer _sprite;

  private void Awake()
  {
    _sprite = this.gameObject.GetComponent<SpriteRenderer>();
  }

  private void OnEnable()
  {
    ResetStats();
  }

  private void ResetStats()
  {

    currentCapacity = 0;
    _sprite.sprite = GarbageStates[0];
    indexState = -1;
    isFull = false;

  }

  //Dito na lang gagamitin magdadagdag ng collider detection.
  private void OnTriggerEnter2D(Collider2D collision)
  {
    Debug.Log("test");
    if (collision.CompareTag("DirtyCup") && !isFull)
    {
      collision.gameObject.SetActive(false);
      CapacityChecker(1f);
      SpawnDirtyCup.amountInScene -= 1;
      return; //in case we have to detect other gameobjects going in the garbage, we have to return so that it won't have to check other tags once this was 


    }
  }

  //Added parameters incase we would dispose food or sticks here and have diff value
  private void CapacityChecker(float numCap)
  {
    float capacityPercentage;

    //check if trash is full
    if (currentCapacity >= trashCapacity)
    {
      Debug.LogWarning("trash is full");
      return;
    }

    currentCapacity += numCap;
    capacityPercentage = (currentCapacity / trashCapacity) * 100;

    Debug.Log(capacityPercentage + "%");

    if (capacityPercentage >= 100)
      isFull = true;

    indexState = -1;
    GarbageSpriteChange(capacityPercentage);
  }

  //make a function that would do the process of changing trash sprites
  private void GarbageSpriteChange(float percentage)
  {

    for (int i = 0; i <= percentage; i += 25)
    {
      indexState++;
    }
    Debug.Log("value of indexState is " + indexState);
    _sprite.sprite = GarbageStates[indexState];

  }


  public void ChangeGarbage()
  {
    StartCoroutine(ChangeGarbageProcess());
  }
  //make a function that would do the procress of changing trash
  private IEnumerator ChangeGarbageProcess()
  {


    yield return new WaitForSeconds(timeForChange);
    ResetStats();
  }



}
