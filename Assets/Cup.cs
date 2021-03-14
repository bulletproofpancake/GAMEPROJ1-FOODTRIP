using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
  private SpriteRenderer cupSprite;

  [SerializeField]
  private Sprite[] sprites = new Sprite[2];




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
        Debug.Log("Stick is detected!");
        cupSprite.sprite = sprites[1];
      }
    }
  }
}
