using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
  private SpriteRenderer cupSprite;

  [SerializeField]
  private Sprite[] sprites = new Sprite[2];


  public List<OrderTT> foodContents;


  public int fishBalls, squidBalls, kwekKwek;

  private void Start()
  {
    cupSprite = GetComponent<SpriteRenderer>();
    cupSprite.sprite = sprites[0];
  }

  private void OnEnable()
  {
    cupSprite = GetComponent<SpriteRenderer>();
    cupSprite.sprite = sprites[0];

  }
  private void OnTriggerStay2D(Collider2D other)
  {
    if (Input.GetKeyDown("mouse 2"))
    {
      if (other.gameObject.CompareTag("Stick"))
      {
        foodContents = other.gameObject.GetComponent<StickBehaviors>()._getFood;
        //other.gameObject.GetComponent<StickBehaviors>()._getFood.Clear();
        TusokTusokFoodSpawner.Instance.currentCapacity -= 3;
        StickSpawner.currentSpawned--;
        GiveOrder();

        Debug.Log("Stick is detected!");
        cupSprite.sprite = sprites[1];
      }
    }
  }

  // This function hopefully only occurs when the current sprite is set to the 2nd sprite
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (cupSprite.sprite == sprites[1])
    {
      if (other.GetComponent<CustomerTT>() && SpawnDirtyCup.amountInScene < SpawnDirtyCup.maxDirtyCupSpawn)
      {
        gameObject.SetActive(false);
        CupSpawner.currentSpawned--;
      }

      if (SpawnDirtyCup.amountInScene >= SpawnDirtyCup.maxDirtyCupSpawn)
      {
        Debug.LogWarning("There are too many dirty cups in scne!");
      }
    }
  }

  private void OnDisable()
  {
    foodContents.Clear();
  }
  private void GiveOrder()
  {
    foreach (var contents in foodContents)
    {
      switch (contents.tusokFoods)
      {
        case TusokType.Fishball:
          fishBalls++;
          break;
        case TusokType.Kwekkwek:
          kwekKwek++;
          break;
        case TusokType.Squidball:
          squidBalls++;
          break;
      }
    }
  }
}
