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
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (Input.GetButtonDown("mouse 2"))
    {
      if (other.gameObject.tag == "Stick")
      {
        Debug.Log("Stick is detected!");
      }
    }
  }
}
