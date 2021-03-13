using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public SeatType type;
    public Transform slot;
    public bool isTaken;

    private void Awake()
    {
        isTaken = false;
    }
}

public enum SeatType
{
    Customer,
    Food
}
