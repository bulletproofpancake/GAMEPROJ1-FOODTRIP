using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TusokType
{
  Kwekkwek, Fishball, Squidball
}

public class OrderTT : MonoBehaviour
{
  [SerializeField]
  private OrderData data;

  public TusokType tusokFoods;

  public OrderData Data => data;

  private SpriteRenderer _sprite;
  // Start is called before the first frame update
  private void Awake()
  {
    _sprite = GetComponent<SpriteRenderer>();
    _sprite.sprite = data.Image;
  }


}
