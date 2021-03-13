using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cart", menuName = "Data/New Cart")]
public class CartData : ScriptableObject
{
    [SerializeField] private CartType type;
    public CartType Type => type;
    [SerializeField] private GameObject cart;
    public GameObject Cart => cart;
}

public enum CartType
{
    Paresan,
    Ihawan,
    Tusoktusok
}
