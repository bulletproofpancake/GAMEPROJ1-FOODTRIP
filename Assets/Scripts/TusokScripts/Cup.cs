using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
  private SpriteRenderer cupSprite;

  [SerializeField]
  private Sprite[] sprites = new Sprite[2];


  public List<OrderTT> foodContents;

  private void Start()
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
      }

      if (SpawnDirtyCup.amountInScene >= SpawnDirtyCup.maxDirtyCupSpawn)
      {
        Debug.LogWarning("There are too many dirty cups in scne!");
      }
    }
  }
}
