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
    [SerializeField] private LocationSprites locSprites;
    public LocationSprites LocSprites => locSprites;
}

public enum CartType
{
    Paresan,
    Tusoktusok
}
