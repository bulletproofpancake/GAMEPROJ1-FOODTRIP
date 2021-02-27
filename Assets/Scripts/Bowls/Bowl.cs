using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public bool isDirty;
    public float washTime;
    public int SeatTaken { get; set; }
    
    private void OnEnable()
    {
        BowlSpawner.Instance.RemoveBowl(gameObject);
    }

    private void OnDisable()
    {
        if(isDirty)
            Sink.Instance.AddBowl(gameObject);
        else
            BowlSpawner.Instance.AddBowl(gameObject); 
    }
}
