using UnityEngine;

public class Seat : MonoBehaviour
{
    public SeatType type;
    public Transform slot;
    public bool isTaken;
}

public enum SeatType
{
    Customer,
    Food
}
